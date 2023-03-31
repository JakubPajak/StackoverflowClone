using System;
namespace StackoveflowClone.Models
{
	public class UserRegisterDto
	{
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PassHash { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Postalcode { get; set; }
    }
}

