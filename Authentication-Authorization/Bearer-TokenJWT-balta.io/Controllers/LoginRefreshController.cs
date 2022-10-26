using Bearer_TokenJWT_balta.io.Models;
using Bearer_TokenJWT_balta.io.Repositories;
using Bearer_TokenJWT_balta.io.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Bearer_TokenJWT_balta.io.Controllers
{
    [ApiController]
    public class LoginRefreshController : ControllerBase
    {
        [HttpPost]
        [Route("loginRefresh")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] User model)
        {
            // Get User from repository
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "User or password invalid!" });

            // If we find the user, we generate the token
            var token = TokenService.GenerateToken(user);
            var refreshToken = TokenService.GenerateRefreshToken();
            TokenService.SaveRefreshToken(user.Username, refreshToken);

            // Hide password from jwt
            user.Password = "";

            return new
            {
                user = user,
                token = token,
                refreshToken = refreshToken
            };
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(string token, string refreshToken)
        {
            var principal = TokenService.GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name;
            var savedRefreshToken = TokenService.GetRefreshToken(username);
            if (savedRefreshToken != refreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            var newJwtToken = TokenService.GenerateToken(principal.Claims);
            var newRefreshToken = TokenService.GenerateRefreshToken();
            TokenService.DeleteRefreshToken(username, refreshToken);
            TokenService.SaveRefreshToken(username, newRefreshToken);

            return new ObjectResult(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken
            });
        }

    }
}
