using System;
using Microsoft.AspNetCore.Mvc;
using StackoveflowClone.Entities;
using StackoveflowClone.Models;
using StackoveflowClone.Services;

namespace StackoveflowClone.Controllers
{
	[Route("user")]
	[ApiController]
	public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
		{
			_userServices = userServices;
		}

		[HttpPost("register")]
		public ActionResult CreateUser([FromBody]UserRegisterDto user)
		{
			_userServices.RegisterUser(user);
			return Ok();
		}

		[HttpPost("login")]
		public ActionResult LoginUser([FromBody]UserLoginDto userdDto)
		{
			string token = _userServices.Login(userdDto);
			return Ok(token);
		}

		[HttpPut("changePass")]
		public ActionResult ChangePassword([FromBody]UserLoginDto userLogin)
		{
			_userServices.ChangePass(userLogin);
			return Ok();
		}
	}
}

