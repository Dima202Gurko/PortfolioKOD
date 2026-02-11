
namespace TestingPlatform.Application.Dtos
{
    public class UserAttemptAnswerDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Правильный ли ответ на вопрос
        /// </summary>
        public bool IsCorrect { get; set; }

        /// <summary>
        /// Сколько баллов получено за ответ (0 или 1)
        /// </summary>
        public int ScoreAwarded { get; set; }

        /// <summary>
        /// К какой попытке привязан выбор
        /// </summary>
        public int AttemptId { get; set; }

        // <summary>
        /// На какой вопрос дан ответ
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Какие были выбраны варианты ответов (или один в случае с единичным выбором)
        /// </summary>
        public List<int>? UserSelectedOptions { get; set; }

        /// <summary>
        /// Если вопрос был текстовый, то здесь храним текстовый ответ
        /// </summary>
        public string? UserTextAnswers { get; set; }
    }
}
