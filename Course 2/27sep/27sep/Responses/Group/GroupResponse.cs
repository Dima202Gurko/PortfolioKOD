using _27sep.Responses.Course;
using _27sep.Responses.Direction;
using _27sep.Responses.Project;

namespace _27sep.Responses.Group
{
    public class GroupResponse : BaseResponse
    {
        /// <summary>
        /// Модель направления
        /// </summary>
        public DirectionResponse Direction { get; set; }

        /// <summary>
        /// Модель курса
        /// </summary>
        public CourseResponse Course { get; set; }

        /// <summary>
        /// Модель проекта
        /// </summary>
        public ProjectResponse Project { get; set; }
    }
}
