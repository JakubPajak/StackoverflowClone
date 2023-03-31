using System;
namespace StackoveflowClone.Entities
{
	public class Post
	{
		public int Id { get; set; }
		public string TextPost { get; set; }
		public string Question { get; set; }
		public int? Points { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }

		public User User { get; set; }
		public int UserId { get; set; }

		public virtual List<Tags> Tags { get; set; } = new List<Tags>() { };
        public virtual List<Coment> Coments { get; set; } = new List<Coment>() { };
    }
}

