using _27sep.Constants;
using System.Security.Authentication;

namespace _27sep.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Получить пользователя из запроса.
        /// </summary>
        /// <param name="httpContext">Контекст запроса.</param>
        /// <returns>Пользователь приложения.</returns>
        public static int TryGetUserId(this HttpContext httpContext)
        {
            var studentIdValue = httpContext.User.Claims.FirstOrDefault(c => c.Type == TestingPlatformClaimTypes.StudentId)?.Value;

            if (!int.TryParse(studentIdValue, out var studentId))
            {
                throw new AuthenticationException("Данные о пользователе пусты.");
            }

            return studentId;
        }
    }
}
