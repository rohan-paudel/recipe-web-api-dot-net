using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.DTOs;

namespace RecipeApp.Controllers
{
	[ApiController]
	[Route("[controller]/[Action]")]
	public class UsersController : ControllerBase
	{
		public RecipeContext _context { get; set; }
		public UsersController(RecipeContext Context)
		{
			_context = Context;
		}


		[HttpPost]
		[Authorize]
		public async Task<ActionResult<UserDTOCreateResponse>> CreateUser([FromBody]UserDTOCreateRequest request)
		{

			await _context.Users.AddAsync(
				new Model.UserModel
				{
					Password = request.Password,
					UserName = request.UserName
				}
				) ;

			await _context.SaveChangesAsync();

			Model.UserModel FoundUser = await _context.Users.Where(x => x.UserName == request.UserName).FirstOrDefaultAsync();

			if (FoundUser != null)
			{
				UserDTOCreateResponse Response = new UserDTOCreateResponse
				{
					Id = FoundUser.Id,
					UserName = FoundUser.UserName,
					CreatedAt = FoundUser.CreatedAt,
					UpdatedAt = FoundUser.UpdatedAt
				};

                return Ok(Response);

            }

			return NotFound(new { message = "Not Found" });
			
            }


            
		}


	}


