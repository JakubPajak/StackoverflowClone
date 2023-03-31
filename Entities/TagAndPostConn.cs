using System;
namespace StackoveflowClone.Entities
{
	public class TagAndPostConn
	{
		public Tags Tag { get; set; }
		public int TagId { get; set; }

		public Post Post { get; set; }
		public int PostId { get; set; }
	}
}

