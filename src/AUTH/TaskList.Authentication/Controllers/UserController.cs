using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using TaskList.Authentication.Domain.Models;
using TaskList.Authentication.Models.User;
using TaskList.Authentication.ResponseModels.Token;
using TaskList.Authentication.ResponseModels.User;
using TaskList.Authentication.Services.Interfaces;

namespace TaskList.Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IClaimsService _claimsService;
        private readonly IJwtTokenService _jwtTokenService;

        public UserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IClaimsService claimsService,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _claimsService = claimsService;
            _jwtTokenService = jwtTokenService;
        }



        [HttpPost]
        [Route("register")]
        [SwaggerOperation(
            Summary = "Add user",
            Description = "Add a user by name, email and password",
            Tags = new[] { "User" }
            )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel userRegister)
        {
            IdentityResult result;

            ApplicationUser newUser = new()
            {
                Email = userRegister.Email,
                UserName = userRegister.Name,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            result = await _userManager.CreateAsync(newUser, userRegister.Password);

            var response = new UserRegisterResponseModel
                (
                    result.Succeeded,
                    result.Errors.Select(e => e.Description)
                );

            if (!result.Succeeded)
            {
                return Conflict(response);
            }

            return CreatedAtAction(nameof(Register), response);
        }

        [HttpPost]
        [Route("login")]
        [SwaggerOperation(
            Summary = "Receive a JWT token",
            Description = "Accept login and password, if there is a match, return a jwt token",
            Tags = new[] { "User" }
            )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userLoginDTO)
        {

            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, userLoginDTO.Password))
            {
                var userClaims = await _claimsService.GetUserClaimsAsync(user);

                var token = _jwtTokenService.GetJwtToken(userClaims);

                return Ok(new UserLoginResponseModel
                (
                    true,
                    "",
                    new TokenResponseModel
                    (
                        new JwtSecurityTokenHandler().WriteToken(token),
                        token.ValidTo
                    )
                ));
            }

            return Unauthorized(new UserLoginResponseModel
            (
                false,
                "The email and password combination was invalid.",
                null
            ));
        }
    }
}
