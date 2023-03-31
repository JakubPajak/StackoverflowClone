using System;
namespace StackoveflowClone.Models
{
	public class CommentDto
	{
        public string Text { get; set; }
        public int? Points { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PostId { get; set; }
    }
}

