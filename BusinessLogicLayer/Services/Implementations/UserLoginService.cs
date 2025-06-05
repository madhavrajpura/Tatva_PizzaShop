using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interfaces;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services.Implementations;

public class UserLoginService : IUserLoginService
{
    private readonly IJWTService _JWTService;
    private readonly IUserLoginRepository _userLoginRepository;

    public UserLoginService(IJWTService JWTService, IUserLoginRepository userLoginRepository)
    {
        _userLoginRepository = userLoginRepository;
        _JWTService = JWTService;
    }

    public async Task<bool> Register(UserLoginViewModel model)
    {
        model.Password = Helper.Encryption.EncryptPassword(model.Password);
        bool result = await _userLoginRepository.Register(model);

        if (result)
        {
            return true;
        }

        return false;
    }

    public async Task<string> Login(UserLoginViewModel model)
    {
        UserLogin? UserLogin = await _userLoginRepository.GetUserEmail(model.Email);
        UserLogin? UserLogins = await _userLoginRepository.GetUserId(UserLogin.Id);

        if (UserLogin == null || UserLogin.Password != Helper.Encryption.EncryptPassword(model.Password) || UserLogins == null)
        {
            return null!;
        }

        string token = _JWTService.GenerateToken(model.Email, UserLogins.Id);
        return token;
    }

}
