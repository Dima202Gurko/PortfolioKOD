using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface IStudentRepository
    {
        /// <summary>
        /// Получить список студентов
        /// </summary>
        /// <returns>Список студентов</returns>
        Task<IEnumerable<StudentDto>> GetAllAsync(
          List<int>? groupIds = null,
          string? searchName = null,
          string? sortBy = null,
          bool sortDescending = true,
          int pageNumber = 1,
          int pageSize = 10
        );

        /// <summary>
        /// Получить студента по UserId
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<StudentDto?> GetStudentByUserId(int userId);



        /// <summary>
        /// Получить студента по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор студента.</param>
        /// <returns>Студент.</returns>
        Task<StudentDto> GetByIdAsync(int id);

        /// <summary>
        /// Добавить нового студента.
        /// </summary>
        /// <param name="studentDto">Модель создания нового студента.</param>
        /// <returns>Идентификатор нового проекта.</returns>
        Task<int> CreateAsync(StudentDto studentDto);

        /// <summary>
        /// Обновить информацию о студенте.
        /// </summary>
        /// <param name="studentDto">Модель обновления студента.</param>
        Task UpdateAsync(StudentDto studentDto);

        /// <summary>
        /// Удалить студента.
        /// </summary>
        /// <param name="id">Идентификатор студента.</param>
        Task DeleteAsync(int id);

        Task UpdateAvatarAsync(StudentDto studentDto);
    }
}
