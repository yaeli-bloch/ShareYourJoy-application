using Microsoft.Extensions.Configuration;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server.Service
{
    public class AuthService :IAuthService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public async Task<string> LoginAsync(Login login)
        {
            // 1. חיפוש קבוצה לפי סיסמא
            await Console.Out.WriteLineAsync(login.Password+"pppppppppp");
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
            await Console.Out.WriteLineAsync(user.Email+"llllllllll");
            int id = user.Id;
            if (group.Users == null || !group.Users.Any(u => u.UserId == id))
            {
                throw new Exception("User not found in group");
            }

            var token = GenerateJwtToken(user);  // כאן תיצור את ה-Token
            return token;
            // 3. אם הכל תקין, ניצור JWT Token למשתמש

        }
        private string GenerateJwtToken(Users user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],  // כאן אתה משתמש ב-Issuer מתוך ה-appsettings
                audience: jwtSettings["Audience"],
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
