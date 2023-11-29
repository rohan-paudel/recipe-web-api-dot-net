using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Model
{
	public class RecipeModel : BaseModel
	{

		[Required, MaxLength(100)]
		public string Title { get; set; }

        [Required, MaxLength(1000)]
        public string Content { get; set; }

        
        public string ImageURL { get; set; }

		[Required]
		public int CreaterId { get; set; }

		[ForeignKey("CreaterId")]
		public UserModel Creater { get; set; }
	}
}

