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

            Mock<UserManager<User>> userManagerMock = new Mock<UserManager<User>>();
            Mock<JwtHandler> jwtHandlerMock = new Mock<JwtHandler> ();

            LoginService loginService = new LoginService(userManagerMock.Object,
                jwtHandlerMock.Object);

            UserDTO userDTO = new UserDTO
            {
                UserName = "ali",
                Password = "P@ssw0rd123456"
            };
            var expected =await loginService.GetAuthorizationAsync(userDTO);

            string Token=null;
  


            Assert.Equal(expected.Token, "Bearer " + Token);

        }
    }
}