using TestingPlatform.Domain.Enum;

namespace _27sep.Responses.Test
{
    public class TestResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRepeatable { get; set; }
        public TestType Type { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset PublishedAt { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public int? DurationMinutes { get; set; }
        public bool IsPublic { get; set; }
        public int? PassingScore { get; set; }
        public int? MaxAttempts { get; set; }
        public List<int> StudentIds { get; set; } = new();
        public List<int> GroupIds { get; set; } = new();
        public List<int> CourseIds { get; set; } = new();
        public List<int> DirectionIds { get; set; } = new();
        public List<int> ProjectIds { get; set; } = new();
    }
}
