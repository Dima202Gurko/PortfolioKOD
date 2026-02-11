using System.ComponentModel.DataAnnotations;
using TestingPlatform.Domain.Enum;

namespace _27sep.Requests.Test
{
    public class CreateTestRequest
    {
        [Required(ErrorMessage = "Название теста обязательно")]
        [StringLength(200, ErrorMessage = "Название не может превышать 200 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Описание теста обязательно")]
        [StringLength(1000, ErrorMessage = "Описание не может превышать 1000 символов")]
        public string Description { get; set; }

        public bool IsRepeatable { get; set; } = false;

        [Required(ErrorMessage = "Тип теста обязателен")]
        public TestType Type { get; set; }

        [Required(ErrorMessage = "Дата публикации обязательна")]
        public DateTime PublishedAt { get; set; }

        [Required(ErrorMessage = "Дедлайн обязателен")]
        public DateTime Deadline { get; set; }

        [Range(1, 480, ErrorMessage = "Длительность должна быть от 1 до 480 минут")]
        public int? DurationMinutes { get; set; }

        public bool IsPublic { get; set; } = false;

        [Range(0, 1000, ErrorMessage = "Проходной балл должен быть от 0 до 1000")]
        public int? PassingScore { get; set; }

        [Range(1, 10, ErrorMessage = "Максимальное количество попыток должно быть от 1 до 10")]
        public int? MaxAttempts { get; set; }

        public List<int> StudentIds { get; set; } = new();
        public List<int> GroupIds { get; set; } = new();
        public List<int> CourseIds { get; set; } = new();
        public List<int> DirectionIds { get; set; } = new();
        public List<int> ProjectIds { get; set; } = new();
    }
}
