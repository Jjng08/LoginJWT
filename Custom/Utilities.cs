using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LoginJWT.Models;

namespace LoginJWT.Custom
{
    public class Utilities
    {
        private readonly IConfiguration _config;
        public Utilities(IConfiguration config)
        {
            _config = config;
        }

        public string encriptarSHA256(string cadena)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(cadena));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public string GRJWT(Usuario Modelo) {
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Modelo.Idusuario.ToString()),
                new Claim(ClaimTypes.Email, Modelo.Correo!),

            };

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:key"]!));

            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256Signature);

            var JWTconfig = new JwtSecurityToken(
                claims:userClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(JWTconfig);
        }
    }
}
