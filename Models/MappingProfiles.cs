
using System;
using AutoMapper;
using StackoveflowClone.Entities;

namespace StackoveflowClone.Models
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<CommentDto, Coment>()
				.ForMember(m => m.Text, c => c.MapFrom(s => s.Text))
				.ForMember(m => m.CreatedDate, c => c.MapFrom(s => s.CreatedDate))
				.ForMember(m => m.PostId, c => c.MapFrom(s => s.PostId))
				.ForMember(m => m.Points, c => c.MapFrom(s => s.Points));
		}
	}
}

