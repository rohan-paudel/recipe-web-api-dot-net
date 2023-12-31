﻿
using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Model
{
	public class BaseModel
	{

		[Key]
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public DateTime UpdatedAt { get; set; } = DateTime.Now;

	}
}

