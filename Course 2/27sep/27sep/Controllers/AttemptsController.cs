using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using _27sep.Extensions;
using _27sep.Requests.Attemp;
using Microsoft.AspNetCore.Authorization;

namespace _27sep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = "Student")]
    public class AttemptsController(IAttemptRepository attemptRepository, IMapper mapper) : ControllerBase
    {
        /// <summary>
        /// Начать попытку
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAttempt(CreateAttemptRequest attempt)
        {
            //TODO: Добавили получение идентификатора студента из контекста
            // поняли, что будет в разных местах использоваться, решили сделать extensions
            // var studentIdValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == TestingPlatformClaimTypes.StudentId)?.Value;
            // if (!int.TryParse(studentIdValue, out var studentId))
            // {
            //     return Unauthorized();
            // }

            var studentId = HttpContext.TryGetUserId();

            var attemptDto = mapper.Map<AttemptDto>(attempt);
            attemptDto.StudentId = studentId;

            var attemptId = await attemptRepository.CreateAsync(attemptDto);

            return StatusCode(StatusCodes.Status201Created, new { Id = attemptId });
        }

        /// <summary>
        /// Закончить попытку
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAttempt(UpdateAttemptRequest attempt)
        {
            //TODO: Добавили получение идентификатора студента из контекста
            // поняли, что будет в разных местах использоваться, решили сделать extensions
            // var studentIdValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == TestingPlatformClaimTypes.StudentId)?.Value;
            // if (!int.TryParse(studentIdValue, out var studentId))
            // {
            //     return Unauthorized();
            // }

            var studentId = HttpContext.TryGetUserId();

            var attemptDto = mapper.Map<AttemptDto>(attempt);
            attemptDto.StudentId = studentId;

            await attemptRepository.UpdateAsync(attemptDto);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
