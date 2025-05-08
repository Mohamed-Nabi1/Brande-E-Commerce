using E_Commerce.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager;

        public TestController(UserManager<Customer> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        [Route("test")]
        public async Task<ActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            return Ok(new string[]
            {
               "Authorization is valid && the user Data is",
               $"FirstName :{user!.FirstName}",
               $"ID: {user.Id}",
               $"Email :{user.Email}"
            }
            );
        }
    }
}
