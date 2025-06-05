using DataAccessLayer.ViewModels;

namespace BusinessLogicLayer.Services.Interfaces;

public interface IUserLoginService
{
    Task<bool> Register(UserLoginViewModel model);
    Task<string> Login(UserLoginViewModel model);

}
