using Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Repositories
{
    public interface IUserRepository
    {
        Task<Users> GetUserByIdAsync(int id);
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users> AddUserAsync(Users user, string passwordGroup);
        Task<Users> UpdateUserAsync(Users user);
        Task DeleteUserAsync(int id);
        Task<Users> GetUserByMail(string email);
    }
}
