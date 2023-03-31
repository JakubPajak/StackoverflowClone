using StackoveflowClone.Entities;
using StackoveflowClone.Models;

namespace StackoveflowClone.Services
{
    public interface ICommentServices
    {
        void CreateComment(CommentDto coment, int id);
        void DeleteComment(int id);
        List<CommentDto> GetPostComments(int id);
        List<Coment> GetUserComments(int id);
    }
}