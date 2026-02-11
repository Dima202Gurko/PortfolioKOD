using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using _27sep.Requests.Student;
using _27sep.Responses.Student;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using _27sep.Extensions;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class StudentsController(IStudentRepository studentRepository, IUserRepository userRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Student, Manager")]
    [ProducesResponseType(typeof(IEnumerable<StudentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStudents(
          [FromQuery] List<int>? groupIds = null,
          [FromQuery] string? searchName = null,
          [FromQuery] string? sortBy = null,
          [FromQuery] bool sortDescending = true,
          [FromQuery] int pageNumber = 1, 
           
          [FromQuery] int pageSize = 10
        )
    {
        var students = await studentRepository.GetAllAsync(
            groupIds,
            searchName,
            sortBy,
            sortDescending,
            pageNumber,
            pageSize
        );

        var response = mapper.Map<IEnumerable<StudentResponse>>(students);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await studentRepository.GetByIdAsync(id);

        return Ok(mapper.Map<StudentResponse>(student));
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest student)
    {
        var userDto = new UserDto()
        {
            Login = student.Login,
            Password = student.Password,
            Email = student.Email,
            FirstName = student.FirstName,
            MiddleName = student.MiddleName,
            LastName = student.LastName,
            Role = UserRole.Student
        };
        var userId = await userRepository.CreateAsync(userDto);

        var studentDto = new StudentDto()
        {
            UserId = userId,
            Phone = student.Phone,
            VkProfileLink = student.VkProfileLink
        };

        var studentId = await studentRepository.CreateAsync(studentDto);

        return StatusCode(StatusCodes.Status201Created, new { Id = studentId });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentRequest student)
    {
        await studentRepository.UpdateAsync(mapper.Map<StudentDto>(student));

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        await studentRepository.DeleteAsync(id);

        return NoContent();
    }
    [HttpPost("avatar")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> UploadAvatar([FromForm] UploadAvatarRequest request)
    {
        var studentId = HttpContext.TryGetUserId();

        var student = await studentRepository.GetByIdAsync(studentId);

        if (request.Avatar.Length == 0)
            return BadRequest("Файл не передан");

        // Папка для аватаров
        var avatarsFolder = Path.Combine("Uploads", "avatars");

        if (!Directory.Exists(avatarsFolder))
            Directory.CreateDirectory(avatarsFolder);

        // Генерируем уникальное имя файла
        var fileExtension = Path.GetExtension(request.Avatar.FileName);
        var oldFileName = Path.GetFileNameWithoutExtension(request.Avatar.FileName);
        var fileName = $"{oldFileName}_{Guid.NewGuid()}{fileExtension}";

        var filePath = Path.Combine(avatarsFolder, fileName);

        // Сохраняем файл
        await using var stream = new FileStream(filePath, FileMode.Create);
        await request.Avatar.CopyToAsync(stream);

        student.AvatarPath = $"/uploads/avatars/{fileName}";

        await studentRepository.UpdateAvatarAsync(student);

        return Ok(new
        {
            avatarUrl = student.AvatarPath
        });
    }
}