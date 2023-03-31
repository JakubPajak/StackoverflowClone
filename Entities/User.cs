using System;
namespace StackoveflowClone.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string PassHash { get; set; }
		public DateTime BirthDate { get; set; }

		public Address Address { get; set; }
		public List<Post> Posts { get; set; } = new List<Post>() { };
		public List<Coment> Coments { get; set; } = new List<Coment>() { };

	}
}

