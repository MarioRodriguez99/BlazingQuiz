using BlazingQuiz.Api.Data;
using BlazingQuiz.Api.Data.Entities;
using BlazingQuiz.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazingQuiz.Api.Service
{
    public class AuthService
    {
        private readonly QuizContext _ctx;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _config;
        public AuthService(QuizContext ctx, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _ctx = ctx;
            _passwordHasher = passwordHasher;
            _config = configuration;
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto login)
        {
            var user = await _ctx.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == login.Username);

            if (user == null)
            {
                return new AuthResponseDto(default, "Invalid user credentials");
            }
            var passwordResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);

            if (passwordResult == PasswordVerificationResult.Failed)
            {
                return new AuthResponseDto(default, "Invalid user password");
            }

            var token = GenerateJWT(user);
            var loggedUser = new LoggedUser(user.Id,user.Name,user.Role, token);
            return new AuthResponseDto(loggedUser, default);

        }

        private string GenerateJWT(User user)
        {
            Claim[] claims =
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                ];
            var secretKey = _config.GetValue<string>("Jwt:Secret");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCred =  new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _config.GetValue<string>("Jwt:Issuer"),
                audience: _config.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:ExpireInMinutes")),
                signingCredentials: signingCred);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        
        }
    }
}
 