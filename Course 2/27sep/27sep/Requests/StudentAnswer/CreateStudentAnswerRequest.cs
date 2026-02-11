namespace _27sep.Requests.StudentAnswer
{
    public class CreateStudentAnswerRequest
    {
        /// <summary>
        /// К какой попытке привязан выбор
        /// </summary>
        public int AttemptId { get; set; }

        /// <summary>
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
