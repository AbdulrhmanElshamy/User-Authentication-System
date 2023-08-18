using Azure;
using User_Authentication_System.Helpers;
using User_Authentication_System.View_Models;

namespace User_Authentication_System.Repos.AuthRepos
{
    public interface IAuthRepo
    {
        Task<ResponseData<UserRegisterVM>> RegisterAsync(UserRegisterVM modek);

        Task<ResponseData<UserLoginVM>> LoginAsync(UserLoginVM model);

        Task<bool> AllowUser(string user);

        Task<bool> AllowEmail(string email);

        Task LogOutAsync();

    }
}
