using Application.Auth.Requests;
using Application.Shared;
using Domain.Entities;
using FluentValidation;
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
            LoginRequestValidator validator = new LoginRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Fail<string>(ErrorCode.ValidationFailed, validationResult);
            }

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return Fail<string>(ErrorCode.UserNotFound, "User not found");
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return Fail<string>(ErrorCode.InvalidUsernameOrPassword, "Invalid UserName or Password");
            }
            var token = await GenerateAccessToken(user);
            return Ok(token);
        }

        public async Task<Response<string>> Register(RegisterRequest request)
        {
            RegisterRequestValidator validator = new RegisterRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Fail<string>(ErrorCode.ValidationFailed, validationResult);
            }

            var userExists = await _userManager.FindByNameAsync(request.UserName);
            if (userExists != null)
                return Fail<string>(ErrorCode.UserAlreadyExists, "User already exists");
            var user = request.Adapt<User>();
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return Fail<string>(ErrorCode.ValidationFailed, result);

            var token = await GenerateAccessToken(user);

            return Ok(token);
        }

        private async Task<string> GenerateAccessToken(User user)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key));

            var token = new JwtSecurityToken(
                       issuer: _config.Issuer,
                       audience: _config.Audience,
                       expires: DateTime.UtcNow.AddDays(1),
                       signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
