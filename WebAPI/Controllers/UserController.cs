using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Logic.Interfaces;
using WebAPI.Core.Model;
using Core.DTO;
using WebAPI.Core.DTO;
using Swashbuckle.Swagger.Annotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("GetUserList")]
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<User> GetUserList()
        {
            return _userService.GetUserList();
        }

        [Route("CreateUser")]
        [AllowAnonymous]
        [HttpPost]
        [SwaggerResponse(200, "OK", typeof(List<ServiceResponceDTO>))]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ServiceResponceDTO> CreateUser([FromBody] UserDTO user)
        {
            return await _userService.CreateUser(user); ;
        }
    }
}
