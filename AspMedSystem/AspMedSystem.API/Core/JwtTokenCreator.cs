using AspMedSystem.DataAccess;
using AspMedSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspMedSystem.API.Core
{
    public class JwtTokenCreator
    {
        private readonly MedSystemContext _context;
        private readonly JWTSettings _settings;

        public JwtTokenCreator(MedSystemContext context, JWTSettings settings)
        {
            _context = context;
            _settings = settings;
        }

        public string Create(string email, string password)
        {
            var foundUser = _context.Users.Where(user => user.Email == email).Select(user => new
            {
                user.BirthDate,
                user.Email,
                user.Password,
                user.FirstName,
                user.LastName,
                user.Id,
                UserAllows = user.UserPermissions.Where(permission => permission.Effect == Effect.Allow).Select(permission => permission.Permission),
                GroupAllows = user.Group.GroupPermissions.Where(permission => permission.Effect == Effect.Allow).Select(permission => permission.Permission),
                UserForbids = user.UserPermissions.Where(permission => permission.Effect == Effect.Disallow).Select(permission => permission.Permission),
            })
            .FirstOrDefault();

            if (foundUser == null)
            {
                throw new UnauthorizedAccessException();
            }

            if (!BCrypt.Net.BCrypt.Verify(password, foundUser.Password))
            {
                throw new UnauthorizedAccessException();
            }

            Guid tokenGuid = Guid.NewGuid();

            string tokenId = tokenGuid.ToString();

            IEnumerable<string> AllowedUseCases =
            [
                ..foundUser.UserAllows,
                ..foundUser.GroupAllows
            ];

            AllowedUseCases = AllowedUseCases.Except(foundUser.UserForbids).Distinct();

            var claims = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Jti, tokenId, ClaimValueTypes.String),
                 new Claim(JwtRegisteredClaimNames.Iss, _settings.Issuer, ClaimValueTypes.String),
                 new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                 new Claim("BirthDate", foundUser.BirthDate.ToString()),
                 new Claim("FirstName", foundUser.FirstName),
                 new Claim("LastName", foundUser.LastName),
                 new Claim("Email", foundUser.Email),
                 new Claim("Id", foundUser.Id.ToString()),
                 new Claim("AllowedUseCases", JsonConvert.SerializeObject(AllowedUseCases)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddSeconds(_settings.Seconds),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
