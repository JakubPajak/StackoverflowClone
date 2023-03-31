using System;
namespace StackoveflowClone.Entities
{
	public class Coment
	{
        public int Id { get; set; }
        public string Text { get; set; }
        public int? Points { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Post Post { get; set; }
        public int PostId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}

