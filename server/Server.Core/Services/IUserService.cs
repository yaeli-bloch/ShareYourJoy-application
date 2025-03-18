using Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Services
{
    public interface IUserService
    {
        Task<Users> GetUserByIdAsync(int id);
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users> CreateUserAsync(Users user,string passwordGroup);
        Task<Users> UpdateUserAsync(Users user);
        Task DeleteUserAsync(int id);   
        Task<Users> GetUserByMail(string email);

    }
}
