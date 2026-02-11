using System.ComponentModel.DataAnnotations;

namespace _27sep.Requests.Group
{
    public class CreateGroupRequest
    {
        /// <summary>
        /// Название группы
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор направления
        /// </summary>
        public int DirectionId { get; set; }

        /// <summary>
        /// Идентфикатор курса
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        public int ProjectId { get; set; }
    }
}
