using System;
using System.Collections.Generic;
using System.Linq;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface ITestResultsRepository
    {
        /// <summary>
        /// Получить все результаты тестов студентов для менеджера.
        /// </summary>
        /// <returns>Массив результатов.</returns>
        Task<List<TestResultDto>> GetAllAsync();

        /// <summary>
        /// Получить все результаты тестов для студента.
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns></returns>
        Task<List<TestResultDto>> GetByStudentIdAsync(int studentId);
    }

}
