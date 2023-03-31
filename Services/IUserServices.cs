using StackoveflowClone.Models;

namespace StackoveflowClone.Services
{
    public interface IUserServices
    {
        string Login(UserLoginDto dto);
        void RegisterUser(UserRegisterDto user);
        void ChangePass(UserLoginDto userLogin);
    }
}