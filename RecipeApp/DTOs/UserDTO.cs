using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeApp.DTOs
{
	public class UserDTOLoginRequest
	{
		public string UserName { get; set; }

		public string Password { get; set; }
	}

	public class UserDTOLoginResponseSuccess
	{
        public int Id { get; set; }

        public string UserName { get; set; }

		public string JwtToken { get; set; }

		public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

	public class UserDTOLoginResponseNotFound
	{
		public int StatusCode { get; set; }

		public string Message { get; set; }
	}


    public class UserDTOCreateRequest
	{
		public string UserName { get; set; }

		public string Password { get; set; }
	}

	public class UserDTOCreateResponse
	{
        public int Id { get; set; }

        public string UserName { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }
	}

}

