using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RecipeApp.Data;
using RecipeApp.DTOs;
using RecipeApp.Model;

namespace RecipeApp.Controllers
{

	[ApiController]
	[Route("[controller]/[Action]")]
	public class AuthController : ControllerBase
	{

		public RecipeContext _context { get; set; }

		public IConfiguration _configuration { get; set; }

		public AuthController(IConfiguration Configuration, RecipeContext Context)
		{
			_configuration = Configuration;
            _context = Context;
		}

		private string GetToken(UserModel User)
		{

			var Section = _configuration.GetSection("Jwt");
            var issuer = Section.GetValue<string>("ValidIssuer");
            var audience = Section.GetValue<string>("ValidAudience");
            var key = Encoding.ASCII.GetBytes
            (Section.GetValue<string>("ValidAudience"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("id", User.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, User.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddMinutes(Section.GetValue<int>("ExpirationMinutes")),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;

        }

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTOLoginResponseSuccess))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(UserDTOLoginResponseNotFound))]
        public async Task<IActionResult> LogIn([FromQuery]string UserName, [FromQuery]string Password)
		{
			UserModel MatchedUser = await _context.Users.Where(x => x.UserName == UserName).FirstOrDefaultAsync();
			if (MatchedUser == null)
			{
				return NotFound(new UserDTOLoginResponseNotFound{ StatusCode = 404, Message = "UserName not found in DataBase"});
			}

			if (MatchedUser.Password != Password)
			{
                return NotFound(new UserDTOLoginResponseNotFound{ StatusCode = 404, Message = "Username or Password didnot matched"});
            }

			UserDTOLoginResponseSuccess Response = new UserDTOLoginResponseSuccess
			{
				Id = MatchedUser.Id,
				UserName = MatchedUser.UserName,
				JwtToken = GetToken(MatchedUser),
				CreatedAt = MatchedUser.CreatedAt,
				UpdatedAt = MatchedUser.UpdatedAt
			};



			return Ok(Response);

		}

	}
}

