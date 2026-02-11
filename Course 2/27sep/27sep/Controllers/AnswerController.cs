using _27sep.Requests.Answer;
using _27sep.Responses.Answer;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Application.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace _27sep.Controllers;
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
[Authorize(Roles = "Manager")]
public class AnswerController : ControllerBase
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IMapper _mapper;

    public AnswerController(IAnswerRepository answerRepository, IMapper mapper)
    {
        _answerRepository = answerRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAnswers()
    {
        var answers = await _answerRepository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<AnswerResponse>>(answers));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAnswerById(int id)
    {
        try
        {
            var answer = await _answerRepository.GetByIdAsync(id);
            return Ok(_mapper.Map<AnswerResponse>(answer));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAnswer([FromBody] CreateAnswerRequest answerRequest)
    {
        var answerId = await _answerRepository.CreateAsync(_mapper.Map<AnswerDto>(answerRequest));
        return StatusCode(StatusCodes.Status201Created, new { Id = answerId });
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAnswer(int id, [FromBody] UpdateAnswerRequest answerRequest)
    {
        try
        {
            if (id != answerRequest.Id)
                return BadRequest("ID в пути и в теле запроса не совпадают");

            await _answerRepository.UpdateAsync(_mapper.Map<AnswerDto>(answerRequest));
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> DeleteAnswer(int id)
    {
        try
        {
            await _answerRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
