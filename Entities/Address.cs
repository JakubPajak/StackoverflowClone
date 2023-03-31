using System;
namespace StackoveflowClone.Entities
{
	public class Address
	{
		public int Id { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string Postalcode { get; set; }

		public User User { get; set; }
		public int UserId { get; set; }
	}
}

