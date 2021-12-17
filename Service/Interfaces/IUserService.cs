using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Core.DTO;
using WebAPI.Core.Model;

namespace Logic.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponceDTO> CreateUser(UserDTO user);
        IEnumerable<User> GetUserList();
    }
}
