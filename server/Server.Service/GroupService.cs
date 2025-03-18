using Microsoft.IdentityModel.Tokens;
using Server.Core.Models;
using Server.Core.Repositories;
using Server.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Service
{
    public class GroupService:IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Group> GetGroupByIdAsync(int id)
        {
            return await _groupRepository.GetGroupByIdAsync(id);
        }
        public async Task<Group> GetGroupByPasswordAsync(string password)
        {
            return await _groupRepository.GetGroupByPasswordAsync(password);
        }
        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return await _groupRepository.GetAllGroupsAsync();
        }

        public async Task<Group> CreateGroupAsync(Group group)
        {
            // אפשר להוסיף כאן לוגיקה להוספת שם ייחודי לקבוצה או כל אימות אחר
            return await _groupRepository.CreateGroupAsync(group);
        }

        public async Task<Group> UpdateGroupAsync(Group group)
        {
            // אפשר להוסיף לוגיקה על האם מותר לעדכן
            return await _groupRepository.UpdateGroupAsync(group);
        }

        public async Task DeleteGroupAsync(int id)
        {
            await _groupRepository.DeleteGroupAsync(id);
        }
        public async Task<string> LoginAsync(Login login)
        {
            // 1. חיפוש קבוצה לפי סיסמא
            var group = await _groupRepository.GetGroupByPasswordAsync(login.Password);
            if (group == null)
            {
                throw new Exception("Invalid group password");
            }

            // 2. בדיקה אם המשתמש נמצא ברשימת המשתמשים של הקבוצה
            //
            //var user = group.Users.FirstOrDefault(u => u.Id == login.Id);
            var user = await _userRepository.GetUserByMail(login.Email);
            if (user == null)
            {
                throw new Exception("User not found in group");
            }

            // 3. אם הכל תקין, ניצור JWT Token למשתמש
            var token = GenerateJwtToken(user);  // כאן תיצור את הפונקציה ליצירת ה-Token
            return token;
        }
        private string GenerateJwtToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",
                claims: new[] { 
                    new Claim(ClaimTypes.Name, user.Email) 
                },
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
