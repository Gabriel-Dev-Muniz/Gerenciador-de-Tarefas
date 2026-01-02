using GerenciadorDeTarefa.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GerenciadorDeTarefa.Service
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GerarToken(Usuario usuario)
        {
            var claims = new[]
{
    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
    new Claim(ClaimTypes.Name, usuario.Nome),
    new Claim(ClaimTypes.Email, usuario.Email),
    new Claim(ClaimTypes.Role, usuario.Role)
};


            var jwtKey = _config["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(jwtKey))
                throw new Exception("JWT:Key não configurada no appsettings.json");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            );


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
