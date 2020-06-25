using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public static class TokenService
    {
        public static string GenerateToken(Usuario usuario){
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, usuario.Nome.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Role.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}