using System.ComponentModel.DataAnnotations;

namespace _27sep.Requests.Answer
{
    public class CreateAnswerRequest
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public bool IsCorrect { get; set; }

        [Required]
        public int QuestionId { get; set; }
    }
}
