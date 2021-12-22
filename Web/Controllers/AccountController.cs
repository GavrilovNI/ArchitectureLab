using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Web.Jwt;

namespace Web.Controllers
{
    [Route(ApiPrefix + "[controller]/[action]")]
    public class AccountController : AdvancedController
    {
        private SignInManager<Areas.Identity.Data.User> _signInManager;
        private UserManager<Areas.Identity.Data.User> _userManager;

        public AccountController(SignInManager<Areas.Identity.Data.User> signInManager, UserManager<Areas.Identity.Data.User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateToken([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = AuthOptions.GetSymmetricSecurityKey();

                var token = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    expires: DateTime.UtcNow.AddHours(AuthOptions.LIFETIME),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );


                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Error(HttpStatusCode.BadRequest, "Wrong email/password");
        }
    }
}
