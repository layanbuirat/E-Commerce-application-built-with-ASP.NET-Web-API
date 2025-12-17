using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHOP.BLL.Services.Interfaces;
using SHOP.DAL.DTO.Requests;

namespace SHOP.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]

        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpPatch("Block/{userId}")]

        public async Task<IActionResult> BlockUser([FromRoute] string userId, [FromBody] int days)
        {
            var result = await _userService.BlockUserAsync(userId, days);
            if (!result)
            {
                return NotFound(new { message = "User not found or could not be blocked." });
            }
            return Ok(new { message = "User blocked successfully." });
        }
        

        [HttpPatch("UnBlock/{userId}")]
        public async Task<IActionResult> UnBlockUser([FromRoute] string userId)
        {
            var result = await _userService.UnBlockUserAsync(userId);
            if (!result)
            {
                return NotFound(new { message = "User not found or could not be unblocked." });
            }
            return Ok(new { message = "User unblocked successfully." });
        }

        [HttpGet("IsBlocked/{userId}")]
        public async Task<IActionResult> IsUserBlocked([FromRoute] string userId)
        {
            var isBlocked = await _userService.IsBlockUserAsync(userId);
            return Ok(new { userId, isBlocked });
        }

        [HttpPatch("ChangeRole/{userId}")]

        public async Task<IActionResult> ChangeUserRole([FromRoute] string userId, [FromBody] ChangeRoleRequest request)
        {
            var result = await _userService.ChangeUserRoleAsync(userId, request.RoleName);
            if (!result)
            {
                return NotFound(new { message = "User not found or could not change role." });
            }
            return Ok(new { message = "User role changed successfully." });
        }

    }
}
