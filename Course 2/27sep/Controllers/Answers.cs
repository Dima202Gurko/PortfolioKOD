using Microsoft.AspNetCore.Mvc;

namespace _27sep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllAnswers() => Ok("Список ответов");

        [HttpGet("{id}")]
        public IActionResult GetTestById(int id)
        {
            if (id == 1) return Ok("Ответ 1");
            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateAnswers() => Created("/api/answers/1", "Создан ответ с ID=1");

        [HttpPut("{id}")]
        public IActionResult UpdateAnswers(int id) => NoContent();

        [HttpDelete("{id}")]
        public IActionResult DeleteAnswers(int id) => NoContent();
    }
}
