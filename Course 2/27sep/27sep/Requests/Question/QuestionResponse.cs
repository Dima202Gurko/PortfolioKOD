using TestingPlatform.Domain.Enum;

namespace _27sep.Requests.Question
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public AnswerType AnswerType { get; set; }
        public bool IsScoring { get; set; }
        public int MaxScore { get; set; }
    }
}
