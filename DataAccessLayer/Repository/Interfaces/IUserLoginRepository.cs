using DataAccessLayer.Models;
using DataAccessLayer.ViewModels;

namespace DataAccessLayer.Repository.Interfaces;

public interface IUserLoginRepository
{
    Task<bool> Register(UserLoginViewModel model);
    Task<UserLogin?> GetUserEmail(string email);
    Task<UserLogin?> GetUserId(int Id);
}
