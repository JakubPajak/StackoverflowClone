using StackoveflowClone.Entities;
using StackoveflowClone.Models;

namespace StackoveflowClone.Services
{
    public interface IPostService
    {
        List<Post> GetAllUserPosts(int UserId);
        List<Post> GetAllPostWithTags(string TagName);
        void CreatePost(PostDto postDto);
        void DeletePost(int Id);
    }
}