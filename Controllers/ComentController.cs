using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackoveflowClone.Entities;
using StackoveflowClone.Models;
using StackoveflowClone.Services;

namespace StackoveflowClone.Controllers
{
	[Route("Comment")]
	[ApiController]
	public class ComentController : ControllerBase
	{
        private readonly ICommentServices _commentServices;

        public ComentController(ICommentServices commentServices)
		{
			_commentServices = commentServices;
		}


		[HttpPost("create/{postId}")]
		[Authorize]
		public ActionResult CreateComment([FromBody]CommentDto dto, [FromRoute]int PostId)
		{
			_commentServices.CreateComment(dto, PostId);
			return Ok();
		}

		[HttpGet("userComments/{UserId}")]
		[Authorize]
		public ActionResult GetAllComments([FromRoute]int UserId)
		{
			return Ok(_commentServices.GetUserComments(UserId));
		}


		[HttpGet("postComments/{PostId}")]
		public ActionResult GetPostComments([FromRoute]int PostId)
		{
			_commentServices.GetPostComments(PostId);
			return Ok();
		}


		[HttpDelete("delete/{ComId}")]
		[Authorize]
		public ActionResult DeleteComment([FromRoute]int ComId)
		{
			_commentServices.DeleteComment(ComId);
			return Ok();
		}
	}
}

