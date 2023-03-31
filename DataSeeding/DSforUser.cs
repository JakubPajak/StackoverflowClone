using System;
using Bogus;
using StackoveflowClone.Entities;

namespace StackoveflowClone.DataSeeding
{
	public class DSforUser
	{
		private readonly DbContextStackoverflow _dbContext;

		public DSforUser(DbContextStackoverflow dbContext)
		{
			_dbContext = dbContext;
		}

		public void SeedUser()
		{
			var locale = "pl";
			var count = new int[] { 1, 2, 3, 4, 5, 6, 7};

			var TagType = new string[]
		   {
			"C",
			"Python",
			"JavaScript",
			"C#",
			".NET",
			"TypeScript",
			"EF",
			"Flask",
			"SQL",
			"MSSQL"
		   };

			var TagData = new Faker<Tags>(locale)
				.RuleFor(t => t.Tag, f => f.PickRandom(TagType));

	        var addressData = new Faker<Address>(locale)
				.RuleFor(a => a.Country, f => f.Address.Country())
				.RuleFor(a => a.City, f => f.Address.City())
				.RuleFor(a => a.Street, f => f.Address.StreetName())
				.RuleFor(a => a.Postalcode, f => f.Address.ZipCode());

			var PostData = new Faker<Post>(locale)
				.RuleFor(p => p.Question, f => f.Lorem.Sentence())
				.RuleFor(p => p.TextPost, f => f.Lorem.Sentences(3))
				.RuleFor(p => p.Points, f => f.PickRandom(count))
				.RuleFor(p => p.CreatedDate, f => f.Date.Recent())
				.RuleFor(p => p.Tags, f => f
					.PickRandom(TagData)
					.Generate(f.PickRandom(count)));


			var userData = new Faker<User>(locale)
				.RuleFor(u => u.FullName, f => f.Name.FullName())
				.RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FullName))
				.RuleFor(u => u.BirthDate, f => f.Person.DateOfBirth)
				.RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
				.RuleFor(u => u.PassHash, f => f.Internet.Password())
				.RuleFor(u => u.Address, f => addressData.Generate())
				.RuleFor(u => u.Posts, f => PostData.Generate(f.PickRandom(count)));
			//.RuleFor(u => u.Coments, f => CommentData.Generate(f.PickRandom(count)));

			var users = userData.Generate(100);
			var ids = users.Count();
			var userid = new List<int>() { };
            var postid = new List<int>() { };
            for (int i = 0; i < ids; i++)
			{
				userid.Add(i + 300);
			}

            for (int i = 0; i < 408; i++)
            {
                postid.Add(i);
            }


            var CommentData = new Faker<Coment>(locale)
                .RuleFor(c => c.Text, f => f.Lorem.Sentences(3))
                .RuleFor(c => c.Points, f => f.PickRandom(count))
                .RuleFor(c => c.CreatedDate, f => f.Date.Recent())
                .RuleFor(c => c.PostId, f => f.PickRandom(postid))
				.RuleFor(c => c.UserId, f => f.PickRandom(userid));

            var commments = CommentData.Generate(300);

            if (!_dbContext.Users.Any())
            {
                _dbContext.AddRange(CommentData);
                _dbContext.SaveChanges();
            }
        }
	}
}

