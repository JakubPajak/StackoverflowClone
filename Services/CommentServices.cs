using System;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using StackoveflowClone.AuthorizationAuthentication;
using StackoveflowClone.Entities;
using StackoveflowClone.Exceptions;
using StackoveflowClone.Models;

namespace StackoveflowClone.Services
{
    public class CommentServices : ICommentServices
    {
        private readonly DbContextStackoverflow _dbContext;
        private readonly AuthenticationSettings _authenticationSetings;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserHttpContextService _userHttpContext;
        private readonly IMapper _mapper;

        public CommentServices(DbContextStackoverflow dbContext, AuthenticationSettings authenticationSetings
            , IAuthorizationService authorizationService, IUserHttpContextService userHttpContext
            , IMapper mapper)
        {
            _dbContext = dbContext;
            _authenticationSetings = authenticationSetings;
            _authorizationService = authorizationService;
            _userHttpContext = userHttpContext;
            _mapper = mapper;
        }

        public void CreateComment(CommentDto dto, int postId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userHttpContext.GetUserId);

            if (user is null)
                throw new BadRequestException("User invalid");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userHttpContext.User, user
                , new ResourceOperationRequirement(ResourceOperation.Create)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbiddenAccessException("Autorization failed");

            var newComment = new Coment()
            {
                Text = dto.Text,
                CreatedDate = DateTime.Now,
                PostId = postId,
                UserId = user.Id
            };

            //var newComent = _mapper.Map<Coment>(newComment);

            _dbContext.Add(newComment);
            _dbContext.SaveChanges();
        }

        public List<Coment> GetUserComments(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);


            if (user is null)
                throw new BadRequestException("User invalid");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userHttpContext.User, user
                , new ResourceOperationRequirement(ResourceOperation.Create)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbiddenAccessException("Autorization failed");

            var comments = _dbContext.Coments.Where(c => c.UserId == user.Id).ToList();
            return comments;
        }

        public List<CommentDto> GetPostComments(int id)
        {
            var comments = _dbContext.Coments.Where(c => c.PostId == id).ToList();

            return new List<CommentDto>() { };
        }

        public void DeleteComment(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userHttpContext.GetUserId);


            if (user is null)
                throw new BadRequestException("User invalid");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userHttpContext.User, user
                , new ResourceOperationRequirement(ResourceOperation.Create)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbiddenAccessException("Autorization failed");

            var comment = _dbContext.Coments.FirstOrDefault(c => c.Id == id);

            if (comment is null)
                throw new NotFoundException("Comment not found");

            _dbContext.Remove(comment);
            _dbContext.SaveChanges();
        }

        //dodawanie punktow do komentarza 
    }
}

