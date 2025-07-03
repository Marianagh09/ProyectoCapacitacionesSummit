using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CAP.Auth
{
	public class StartJWT
	{
		private readonly IConfiguration? _configuration;
		private readonly String? _jwtKey;
		private readonly String? _jwtIssuer;
		private readonly String? _jwtAudience;

		public StartJWT(IConfiguration configuration)
		{
			_configuration = configuration;
			// Leer la configuración del JWT desde appsettings.json
			_jwtKey = _configuration["Jwt:Key"];
			_jwtIssuer = _configuration["Jwt:Issuer"];
			_jwtAudience = _configuration["Jwt:Audience"];

			if (string.IsNullOrEmpty(_jwtKey) || string.IsNullOrEmpty(_jwtIssuer) || string.IsNullOrEmpty(_jwtAudience))
			{
				throw new ArgumentNullException("La configuración del JWT (Key, Issuer, Audience) no puede ser nula o vacía en appsettings.json.");
			}
		}

		public String GenerateToken(String? email, String? userName, String? roles )
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
			var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Name, userName)
			};

			if (!string.IsNullOrEmpty(roles))
			{ 
					claims.Add(new Claim(ClaimTypes.Role, roles));
				
			}

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(60),
				Issuer = _jwtIssuer,
				Audience = _jwtAudience,
				SigningCredentials = credentials
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}

		public ClaimsPrincipal? ValidateToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var validationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
				ValidateIssuer = true,
				ValidIssuer = _jwtIssuer,
				ValidateAudience = true,
				ValidAudience = _jwtAudience,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};


			try
			{
				SecurityToken validatedToken;
				var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
				return principal;
			}
			catch (SecurityTokenExpiredException)
			{
				return null;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
