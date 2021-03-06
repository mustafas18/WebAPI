using Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using WebAPI.Core.DTO;
using WebAPI.Core.Model;

namespace Logic.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtHandler _jwtHandler;
        public LoginService(UserManager<User> userManager, IJwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }
        /// <summary>
        /// Get Authorization
        /// </summary>
        /// <param name="LoginUser"></param>
        /// <returns>Access token</returns>
        public async Task<LoginResponceDTO> GetAuthorizationAsync(UserDTO LoginUser)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(LoginUser.UserName);
                if (user == null || !await _userManager.CheckPasswordAsync(user, LoginUser.Password))
                    return new LoginResponceDTO { IsAuthSuccessful = false, ErrorMessage = "Invalid Authentication" };

                var signingCredentials = _jwtHandler.GetSigningCredentials();
                var claims = _jwtHandler.GetClaims(user);
                var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return new LoginResponceDTO { IsAuthSuccessful = true, Token = "Bearer " + token };
            }
            catch (Exception ex)
            {
                return new LoginResponceDTO { IsAuthSuccessful = false, ErrorMessage = ex.Message };
            }


        }
    }
}
