using System;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Model;

namespace RecipeApp.Data
{
	public class RecipeContext : DbContext
	{

		public RecipeContext(DbContextOptions options): base(options) {}

		public DbSet<UserModel> Users { get; set; }

		public DbSet<RecipeModel> Recipes { get; set; }
	}
}

