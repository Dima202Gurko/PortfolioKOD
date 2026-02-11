using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPlatform.Application.Dtos
{
    public class TestResultDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Пройден ли тест студентом
        /// </summary>
        public bool Passed { get; set; }

        /// <summary>
        /// К какому тесту отночится результат
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// Лучшая попытка
        /// </summary>
        public int AttemptId { get; set; }

        /// <summary>
        /// Лучший результат
        /// </summary>
        public int BestScore { get; set; }

        /// <summary>
        /// Какого ученика результат
        /// </summary>
        public int StudentId { get; set; }
    }
}
