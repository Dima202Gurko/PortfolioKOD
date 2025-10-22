using System.Text.Json.Serialization;
using TestingPlatform.Domain.Enum;
namespace TestingPlatform.Domain.Models
{
    public class User
    {
        public string PasswordHash { get; set; }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }

        public UserRole Role { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        [JsonIgnore]
        public Student? Student { get; set; }

    }
}
