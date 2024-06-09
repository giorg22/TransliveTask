using Application.Auth.Requests;
using Application.Shared;
using Domain.Entities;
using Infrastructure.Configs;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Application.Shared.ReponseExtensions;

namespace Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthSettings _config;
        public AuthService(
            IOptions<AuthSettings> config,
            UserManager<User> userManager)
        {
            _config = config.Value;
            _userManager = userManager;
        }

        public async Task<Response<string>> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return Fail<string>(ErrorCode.InvalidUsernameOrPassword, "Invalid username or password");
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return Fail<string>(ErrorCode.InvalidUsernameOrPassword, "Invalid username or password");
            }
            var token = await GenerateAccessToken(user);
            return Ok(token);
        }

        public async Task<Response<string>> Register(RegisterRequest request)
        {
            var userExists = await _userManager.FindByNameAsync(request.UserName);
            if (userExists != null)
                return Fail<string>(ErrorCode.AlreadyExists, "User already exists");
            var user = request.Adapt<User>();
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return Fail<string>(result);

            var token = await GenerateAccessToken(user);

            return Ok(token);
        }

        private async Task<string> GenerateAccessToken(User user)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("gc4laaKZovDdezpp6xRUBDPO7ow9j1Qw"));

            var token = new JwtSecurityToken(
                       issuer: "https://localhost:7112",
                       audience: "https://localhost:7112",
                       expires: DateTime.UtcNow.AddDays(1),
                       signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
