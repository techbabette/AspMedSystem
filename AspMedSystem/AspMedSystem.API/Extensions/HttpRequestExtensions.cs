using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AspMedSystem.API.Extensions
{
    public static class HttpRequestExtensions
    {
        public static Guid? GetTokenId(this HttpRequest request)
        {
            var claims = GetTokenClaims(request);

            if(claims == null)
            {
                return null;
            }

            var claim = claims.First(x => x.Type == "jti").Value;

            var tokenGuid = Guid.Parse(claim);

            return tokenGuid;
        }
        public static IEnumerable<Claim> GetTokenClaims(this HttpRequest request)
        {
            if (request == null || !request.Headers.ContainsKey("Authorization"))
            {
                return null;
            }

            string authHeader = request.Headers["Authorization"].ToString();

            if (authHeader.Split("Bearer ").Length != 2)
            {
                return null;
            }

            string token = authHeader.Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            var claims = tokenObj.Claims;

            return claims;
        }
    }
}
