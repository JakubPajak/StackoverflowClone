using System;
using StackoveflowClone.Entities;

namespace StackoveflowClone.Models
{
	public class PostDto
	{
        public string TextPost { get; set; }
        public string Question { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Tags { get; set; }
    }
}

