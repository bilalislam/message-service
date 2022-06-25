using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MessageService.Api
{
    public class TokenProxy : ITokenProxy
    {
        IConfiguration Configuration { get; set; }
        public TokenProxy(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            Token tokenInstance = new Token()
            {
                Expiration = DateTime.Now.AddMinutes(5)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = Configuration["Token:Issuer"],
                Audience = Configuration["Token:Audience"],
                Subject = new ClaimsIdentity(new[] {
                        new Claim("id", user.Id.ToString()),
                        new Claim("name", user.Name.ToString()),
                        new Claim("email", user.Email.ToString()),
                        new Claim("surname", user.Surname.ToString())
                    }),
                Expires = tokenInstance.Expiration,
                NotBefore = DateTime.Now,
                SigningCredentials = signingCredentials,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            tokenInstance.AccessToken = tokenHandler.WriteToken(token);
            tokenInstance.RefreshToken = CreateRefreshToken();

            return tokenInstance;
        }

        private string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
