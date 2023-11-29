using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Model
{
	public class UserModel : BaseModel
	{

		[Required, MinLength(2), MaxLength(50)]
		public string UserName { get; set; }

		[Required, MaxLength(512)]
		public string Password { get; set; }

	}
}

