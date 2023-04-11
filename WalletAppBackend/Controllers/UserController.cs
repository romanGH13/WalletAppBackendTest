using Microsoft.AspNetCore.Mvc;
using WalletAppBackend.Entities;
using WalletAppBackend.Extensions;
using WalletAppBackend.Models;
using WalletAppBackend.Services;

namespace WalletAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            User? user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            DateTime date = DateTime.UtcNow;

            string dailyPoints = _userService.CalculateDailyPoints();

            GetUserResponse response = new GetUserResponse()
            {
                CardBalance = user.CardBalance,
                UserName = user.UserName,
                AvailableBalance = UserService.MaxLimit - user.CardBalance,
                PaymentDue = $"You’ve paid your {date.ToMonthName()} balance.",
                DailyPoints = dailyPoints,
                Id = user.Id
            };

            return Ok(response);
        }

        [HttpGet("list/{page}/{pageCount}")]
        public IActionResult GetUsers(int page = 1, int pageCount = 10)
        {
            PagedResponse<User> response = _userService.GetUsers(page, pageCount);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            User resultUser = await _userService.AddUser(addUserRequest.UserName);

            AddUserResponse response = new AddUserResponse() { Id = resultUser.Id };
            return Ok(response);
        }

    }
}
