using Bearer_TokenJWT_balta.io.Models;
using Bearer_TokenJWT_balta.io.Repositories;
using Bearer_TokenJWT_balta.io.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bearer_TokenJWT_balta.io.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        // dynamic -> type that will return nothing
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] User model)
        {
            // Get User from repository
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "User or password invalid!" });

            // If we find the user, we generate the token
            var token = TokenService.GenerateToken(user);

            // Hide password from jwt
            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }
    }
}
