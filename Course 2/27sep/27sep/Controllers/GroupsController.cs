using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Domain.Models;
using TestingPlatform.Infrastructure;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Repositories;
using TestingPlatform.Application.Dtos;

namespace _27sep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController(IGroupRepository groupRepository) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllGroups()
        {
            var groups = groupRepository.GetAll();

            return Ok(groups);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetGroupById(int id)
        {
            var group = groupRepository.GetById(id);

            return Ok(group);
        }

        [HttpPost]
        public IActionResult CreateGroup([FromBody] GroupDto group)
        {
            var id = groupRepository.Create(group);

            return StatusCode(StatusCodes.Status201Created, new { Id = id });
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateGroup([FromBody] GroupDto group)
        {
            groupRepository.Update(group);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteGroup(int id)
        {
            groupRepository.Delete(id);

            return NoContent();
        }
    }
}
