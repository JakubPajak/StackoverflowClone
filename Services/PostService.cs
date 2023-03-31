using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StackoveflowClone.AuthorizationAuthentication;
using StackoveflowClone.Entities;
using StackoveflowClone.Exceptions;
using StackoveflowClone.Models;

namespace StackoveflowClone.Services
{
    public class PostService : IPostService
    {
        private readonly DbContextStackoverflow _dbContext;
        private readonly AuthenticationSettings _authenticationSetings;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserHttpContextService _userHttpContext;
        private readonly IMapper _mapper;

        public PostService(DbContextStackoverflow dbContext, AuthenticationSettings authenticationSetings
            , IAuthorizationService authorizationService, IUserHttpContextService userHttpContext
        , IMapper mapper)
        {
            _dbContext = dbContext;
            _authenticationSetings = authenticationSetings;
            _authorizationService = authorizationService;
            _userHttpContext = userHttpContext;
            _mapper = mapper;
        }

        public List<Post> GetAllUserPosts(int UserId)
        {
            var posts = _dbContext.Posts.Where(p => p.UserId == UserId).ToList();

            return posts;
        }

        public List<Post> GetAllPostWithTags(string TagName)
        {
            var result = new List<Post>() { };
            var posts = new List<TagAndPostConn>() { };
            var tags = _dbContext.Tags.Where(t => t.Tag == TagName).ToList();

            if (tags is null)
                throw new NotFoundException("Invalid Tag");

            foreach (var tag in tags)
            {
                posts.AddRange(_dbContext.TagsAndPosts.Where(t => t.TagId == tag.Id).ToList());
            }

            if (posts is null)
                throw new NotFoundException("No Posts with this tag yet");

            foreach (var post in posts)
            {
                result.AddRange(_dbContext.Posts.Where(p => p.Id == post.PostId).ToList());
            }

            return result;
        }


        public void CreatePost(PostDto postDto)
        {
            var tagsToDB = new List<Tags>() { };
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userHttpContext.GetUserId);

            if (user is null)
                throw new NotFoundException("User not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userHttpContext.User, user
                , new ResourceOperationRequirement(ResourceOperation.Create)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbiddenAccessException("Authorization Invalid");

            var tagsFromDto = postDto.Tags.Split(',').ToList();

            foreach (var tag in tagsFromDto)
            {
                //dodac sprawdzenie czy taki tag juz istnieje, jesli tak to nie dodaje nowego
                var Tag = new Tags()
                {
                    Tag = tag
                };

                tagsToDB.Add(Tag);
            }

            var newPost = new Post()
            {
                UserId = (int)_userHttpContext.GetUserId,
                TextPost = postDto.TextPost,
                Question = postDto.Question,
                CreatedDate = DateTime.Now,
                Tags = tagsToDB
            };

            _dbContext.Add(newPost);
            _dbContext.SaveChanges();
        }

        public void DeletePost(int Id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userHttpContext.GetUserId);


            if (user is null)
                throw new BadRequestException("User invalid");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userHttpContext.User, user
                , new ResourceOperationRequirement(ResourceOperation.Create)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbiddenAccessException("Autorization failed");

            var post = _dbContext.Posts.FirstOrDefault(c => c.Id == Id);

            if (post is null)
                throw new NotFoundException("Comment not found");

            _dbContext.Remove(post);
            _dbContext.SaveChanges();
        }
    }
}

