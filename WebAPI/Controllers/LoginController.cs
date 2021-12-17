using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using WebAPI.Core.DTO;
using WebAPI.Core.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly ILoginService _loginServicer;
        public LoginController(ILoginService loginService)
        {
            _loginServicer = loginService;
        }
        [HttpPost]
        [Route("GetAuthorization")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAuthorization([FromBody] UserDTO LoginUser)
        {

            LoginResponceDTO loginResponce = await _loginServicer.GetAuthorization(LoginUser);

            if (loginResponce.IsAuthSuccessful)
            {
                return Ok(new LoginResponceDTO { IsAuthSuccessful = true, Token = loginResponce.Token });
            }
            else
            {
                return Unauthorized(new LoginResponceDTO { ErrorMessage = "Invalid Authentication" });
            }
           
        }
    }
}
