using Microsoft.AspNetCore.Mvc;
using Server.API.ModelsDto;
using Server.Core.Models;
using Server.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("{id}", Name = "GetGroupById")]
        public async Task<ActionResult<Group>> GetGroupByIdAsync(int id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }
        [HttpGet("password/{password}")]
        public async Task<ActionResult<Group>> GetGroupByPasswordAsync(string password)
        {
            var group = await _groupService.GetGroupByPasswordAsync(password);
            if (group == null)
            {

                return NotFound();
            }
            return Ok(group);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetAllGroupsAsync()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpPost]
        public async Task<ActionResult<Group>> CreateGroupAsync([FromBody] GroupDTO group)
        {
            if (group == null)
            {
                return BadRequest();
            }
            var GroupToAdd= new Group { Id = group.Id,Name=group.Name,Password=group.Password,AdminPassword=group.AdminPassword };
            var createdGroup = await _groupService.CreateGroupAsync(GroupToAdd);
            return CreatedAtAction("GetGroupById", new { id = createdGroup.Id }, createdGroup);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Group>> UpdateGroupAsync(int id, [FromBody] Group group)
        {
            if (id != group.Id)
            {
                return BadRequest();
            }

            var updatedGroup = await _groupService.UpdateGroupAsync(group);
            return Ok(updatedGroup);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroupAsync(int id)
        {
            await _groupService.DeleteGroupAsync(id);
            return NoContent();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Email or password is missing.");
            }

            try
            {
                // קריאה לפונקציה ב-Service כדי לבצע את הלוגין
                var token = await _groupService.LoginAsync(login);

                // החזרת ה-token כתגובה
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // במקרה של שגיאה, נחזיר תשובה עם הודעת שגיאה
                return Unauthorized(new { Message = ex.Message });
            }
        }
    }
}
