using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ComfyCatalogAPI.Utils
{
    /// <summary>
    /// Visa implementar as funções relacionadas com JWT Tokens (autorização)
    /// </summary>
    public class JwtUtils
    {
        private readonly IConfiguration _configuration;
        public JwtUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Função que visa gerar um JWT token com uma determinada role (User, Admin, etc.)
        /// </summary>
        /// <param name="role">Role que estará presente no token</param>
        /// <returns>String com o token gerado</returns>
        /// 

        public string GenerateJWTToken(string role)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120); // valid for 2 hours
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(ClaimTypes.Role, role));


            var token = new JwtSecurityToken(issuer: issuer, audience: audience, claims: permClaims,
            expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;

        }

        public ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                SecurityToken validatedToken;
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                return claimsPrincipal;
            }
            catch (Exception ex)
            {
                // Token validation failed
                // Handle the exception or return null, depending on your needs
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public string GetUserIDByToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey
            };

            try
            {
                var jwtUtils = new JwtUtils(_configuration);

                // Validate and parse the token
                var claimsPrincipal = jwtUtils.ValidateToken(token, validationParameters);

                // Retrieve the user ID claim from the token
                var userIDClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIDClaim != null)
                {
                    return userIDClaim.Value;
                }
            }
            catch (Exception ex)
            {
                // Token validation failed
                // Handle the exception or return null, depending on your needs
                Console.WriteLine(ex.Message);
            }

            return null; // User ID not found in the token or token validation failed
        }



    }
}
