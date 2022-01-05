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
        public async void LoginService_Return_InvalidAuthorization()
        {
            Mock<FakeUserManager> userManagerMock= new Mock<FakeUserManager>();
            Mock<IJwtHandler> jwtHandlerMock= new Mock<IJwtHandler>();

            UserDTO userDTO = new UserDTO
            {
                UserName = "ali",
                Password = "P@ssw0rd123456"
            };

            User user = new User { UserName = userDTO.UserName, PasswordHash = "secret" };

            var claims = new List<Claim>();
            JwtSecurityToken tokenOptions = new JwtSecurityToken();
            var key = Encoding.UTF8.GetBytes("secretSecuretyKey");
            var secret = new SymmetricSecurityKey(key);
            SigningCredentials signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

         
            userManagerMock.Setup(p => p.CheckPasswordAsync(user, userDTO.Password)).ReturnsAsync(true);

            jwtHandlerMock.Setup(p => p.GetClaims(user)).Returns(new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) });
            jwtHandlerMock.Setup(x => x.GenerateTokenOptions(signingCredentials, claims)).Returns(tokenOptions);
           

            LoginService loginService = new LoginService(userManagerMock.Object, jwtHandlerMock.Object);
            LoginResponceDTO expected =await loginService.GetAuthorizationAsync(userDTO);

            //Assert
            Assert.Equal(expected.ErrorMessage, "Invalid Authentication");

        }
        [Fact]
        public async void LoginService_Should_Return_jwtToken()
        {

            Mock<FakeUserManager> userManagerMock = new Mock<FakeUserManager>();
            Mock<IJwtHandler> jwtHandlerMock = new Mock<IJwtHandler>();



            var claims = new List<Claim>();
            JwtSecurityToken tokenOptions = new JwtSecurityToken();
            var key = Encoding.UTF8.GetBytes("secretSecuretyKey");
            var secret = new SymmetricSecurityKey(key);
            SigningCredentials signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            UserDTO userDTO = new UserDTO
            {
                UserName = "ali",
                Password = "P@ssw0rd123456"
            };

            User user =new User {UserName= userDTO.UserName ,PasswordHash= "secret" };

            userManagerMock.Setup(p => p.CheckPasswordAsync(user,userDTO.UserName)).ReturnsAsync(true);

            jwtHandlerMock.Setup(p => p.GetClaims(user)).Returns(new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) });
            jwtHandlerMock.Setup(x => x.GenerateTokenOptions(signingCredentials, claims)).Returns(tokenOptions);
  

            LoginService loginService = new LoginService(userManagerMock.Object,
                jwtHandlerMock.Object);
            var expected = await loginService.GetAuthorizationAsync(userDTO);

         
               var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);


            Assert.Equal(expected.Token, "Bearer " + token);
            

        }
    }
}