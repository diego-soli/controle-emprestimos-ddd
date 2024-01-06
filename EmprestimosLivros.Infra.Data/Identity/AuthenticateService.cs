using EmprestimoLivros.Domain.Account;
using EmprestimoLivros.Domain.Entities;
using EmprestimosLivros.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EmprestimosLivros.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configurarion;

        public AuthenticateService(ApplicationDbContext context, IConfiguration configurarion)
        {
            _context = context;
            _configurarion = configurarion;
        }

        public async Task<bool> AuthenticateAsync(string email, string senha)
        {
            var usuario = await _context.Usuario.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if (usuario == null)
            {
                return false;
            }
            using var hmac = new HMACSHA512(usuario.PasswordSalt);
            var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
            for (int x = 0; x < computerHash.Length; x++)
            {
                if (computerHash[x] != usuario.PasswordHash[x]) return false;
            }
            return true;
        }

        public string GenerateToken(int id, string email)
        {
            var claims = new[]
            {
                new Claim("id",id.ToString()),
                new Claim("email",email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurarion["jwt:secretKey"]));
            var credentials = new SigningCredentials(privateKey,SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configurarion["jwt:issuer"],
                audience: _configurarion["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            return await _context.Usuario.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<bool> UserExists(string email)
        {
            var usuario = await _context.Usuario.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if (usuario == null)
            {
                return false;
            }
            return true;
        }
    }
}
