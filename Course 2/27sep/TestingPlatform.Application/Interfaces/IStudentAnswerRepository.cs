using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface IStudentAnswerRepository
    {
        /// <summary>
        /// Добавить ответ на вопрос
        /// </summary>
        /// <param name="userAttemptAnswerDto">Модель ответа на вопрос</param>
        /// <returns></returns>
        Task CreateAsync(UserAttemptAnswerDto userAttemptAnswerDto);
    }
}
