using System;
using Microsoft.EntityFrameworkCore;

namespace StackoveflowClone.Entities
{
	public class DbContextStackoverflow : DbContext
	{
        private readonly string Con = "Server=localhost,1433;Database=StackoverflowDB;User=sa; Password=reallyStrongPwd123;TrustServerCertificate=true";
        public DbSet<User> Users { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Tags> Tags { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<TagAndPostConn> TagsAndPosts { get; set; }
		public DbSet<Coment> Coments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptions)
		{
			dbContextOptions.UseSqlServer(Con);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
		}

	}
}

