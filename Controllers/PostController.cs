using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackoveflowClone.Entities;
using StackoveflowClone.Models;
using StackoveflowClone.Services;

namespace StackoveflowClone.Controllers
{
	[Route("Post")]
	[ApiController]
	public class PostController : ControllerBase
	{
        private readonly IPostService _postService;

        public PostController(IPostService postService)
		{
			_postService = postService;
		}

		[HttpGet("getAll/{UserId}")]
		public ActionResult GetAllPosts([FromRoute]int UserId)
		{
			return Ok(_postService.GetAllUserPosts(UserId));
		}

		[HttpGet("getAllFromTag/{tagName}")]
		public ActionResult GetAllFromTag([FromRoute]string TagName)
		{
			return Ok(_postService.GetAllPostWithTags(TagName));
		}

		[HttpPost("create")]
		[Authorize]
		public ActionResult CreateNewPost([FromBody]PostDto postDto)
		{
			_postService.CreatePost(postDto);
			return Ok();
		}

		[HttpDelete("delete/{Id}")]
		[Authorize]
		public ActionResult DeletePost([FromRoute]int Id)
		{
			_postService.DeletePost(Id);
			return Ok();
		}
	}
}

