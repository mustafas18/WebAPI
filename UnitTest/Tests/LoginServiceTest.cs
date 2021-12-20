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

            Mock<FakeUserManager> userManagerMock = new Mock<FakeUserManager>();
            Mock<IJwtHandler> jwtHandlerMock = new Mock<IJwtHandler>();

            User user = new User();
            var claims = new List<Claim>();
            JwtSecurityToken tokenOptions=new JwtSecurityToken ();
            var key = Encoding.UTF8.GetBytes("secretSecuretyKey");
            var secret = new SymmetricSecurityKey(key);
            SigningCredentials signingCredentials=new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            UserDTO userDTO = new UserDTO
            {
                UserName = "ali",
                Password = "P@ssw0rd123456"
            };
            LoginService loginService = new LoginService(userManagerMock.Object,
                jwtHandlerMock.Object);
            var expected = await loginService.GetAuthorizationAsync(userDTO);

            userManagerMock.Setup(x => x.FindByNameAsync(userDTO.UserName)).ReturnsAsync(user);
            jwtHandlerMock.Setup(x => x.GetClaims(user)).Returns(claims);
            jwtHandlerMock.Setup(x => x.GenerateTokenOptions(signingCredentials, claims)).Returns(tokenOptions);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);


            Assert.Equal(expected.Token, "Bearer " + token);
            

        }
    }
}