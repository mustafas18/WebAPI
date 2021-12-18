using Xunit;
using WebAPI.Core.DTO;
using Logic.Interfaces;
using Moq;
using Logic.Services;
using Microsoft.AspNetCore.Identity;
using WebAPI.Core.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UnitTest
{
    public class LoginServiceTest
    {

        [Fact]
        public async void LoginService_Should_Return_jwtToken()
        {
            // Arrange
            Mock<ILoginService> loginService = new Mock<ILoginService>();
            string Token=null;
            UserDTO userDTO = new UserDTO
            {
                UserName = "ali",
                Password = "P@ssw0rd123456"
            };
            var expected=new LoginResponceDTO();
            loginService.Setup(x => x.GetAuthorizationAsync(userDTO)).ReturnsAsync(expected);

            // Act
            User user = new User();
            List<Claim> claims = new List<Claim>();
     

            // Assert
            Assert.Equal(expected.Token, "Bearer " + Token);

        }
    }
}