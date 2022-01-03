using Core.DTO;
using Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core.DTO;
using WebAPI.Core.Model;
using WebAPI.DAL;

namespace Logic.Services
{
    public class UserService : IUserService
    {
        private readonly AppDBContext _context;
        private readonly UserManager<User> _userManager;
        public UserService(AppDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ServiceResponceDTO> CreateUser(UserDTO user)
        {
            try
            {
                var result = _context.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();
                if (result == null)
                {

                    User newUser = new User();
                    newUser.UserName = user.UserName;

                    await _userManager.CreateAsync(newUser, user.Password);
                    await _userManager.AddToRoleAsync(newUser, "admin");

                    return new ServiceResponceDTO { IsSuccessful = true };
                }
                else
                {
                    return new ServiceResponceDTO { IsSuccessful = true, ErrorMessage = "The user is exists." };
                }

            }
            catch (Exception ex)
            {
                return new ServiceResponceDTO { IsSuccessful = false, ErrorMessage = ex.Message };

            }


        }
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUser(string id)
        {
            return _context.Users.Where(s=>s.Id==id).FirstOrDefault();
        }
        /// <summary>
        /// List all users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUserList()
        {
            return _context.Users.ToList();
        }
    }
}
