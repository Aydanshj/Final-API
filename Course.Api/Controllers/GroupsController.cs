using Course.Core.Entities;
using Course.Core.Repositories;
using Course.Service.Dtos.GroupDtos;
using Course.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpGet("all")]
        public ActionResult<List<GroupGetAllItemDto>> GetAll()
        {
            return Ok(_groupService.GetAll());
        }
        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> Get(int id)
        {
            return Ok(_groupService.GetById(id));
        }
        [HttpPost("")]
        public IActionResult Create(GroupCreateDto groupDto)
        {
            var result = _groupService.Create(groupDto);
            return StatusCode(201, result);
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, GroupEditDto brandDto)
        {
            _groupService.Edit(id, brandDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _groupService.Remove(id);
            return NoContent();
        }
    }
}
