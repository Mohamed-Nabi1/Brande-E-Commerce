using E_Commerce.BL;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityManager securityManager;

        public SecurityController(ISecurityManager _securityManager)
        {
            securityManager = _securityManager;
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult Register(RegisterDto register)
        {
            var request = securityManager.Register(register);
            if (request != null)
            {
                return BadRequest(request);
            }

            return NoContent();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult<TokenDto> Login(LoginDto login)
        {
            var request = securityManager.Login(login);
            if (request is null)
            {
                return BadRequest();
            }

            return request;
        }
    }
}
