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
        /// <summary>
        /// List all users
        /// </summary>
        /// <returns></returns>
        [Route("GetUserList")]
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<User> GetUserList()
        {
            return _userService.GetUserList();
        }
        /// <summary>
        /// Find user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id?}")]
        [AllowAnonymous]
        [HttpGet]
        public User GetUser(string id)
        {
            return _userService.GetUser(id);
        }
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("CreateUser")]
        [AllowAnonymous]
        [HttpPost]
        [SwaggerResponse(200, "OK", typeof(List<ServiceResponceDTO>))]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ServiceResponceDTO> CreateUser([FromBody] UserDTO user)
        {
            return await _userService.CreateUser(user); ;
        }
        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("EditUser")]
        [AllowAnonymous]
        [HttpPut]
        public async Task<ServiceResponceDTO> EditUser(UserDTO userDto, string userId)
        {
            return await _userService.EditUser(userDto, userId);
        }

    }
}
