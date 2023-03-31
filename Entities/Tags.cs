using System;
namespace StackoveflowClone.Entities
{
	public class Tags
	{
		public int Id { get; set; }
		public string Tag { get; set; }

		public virtual List<Post> Post { get; set; }
	}
}

