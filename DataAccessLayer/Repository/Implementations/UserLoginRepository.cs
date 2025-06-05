using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interfaces;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Implementations;

public class UserLoginRepository : IUserLoginRepository
{
    private readonly ApplicationDBContext _db;
    public UserLoginRepository(ApplicationDBContext db)
    {
        _db = db;
    }

    public async Task<bool> Register(UserLoginViewModel model)
    {
        UserLogin? user = await _db.UserLogins.FirstOrDefaultAsync(e => e.Email == model.Email);

        if (user != null)
        {
            return false;
        }

        UserLogin? userlogin = new UserLogin();
        userlogin.Name = model.Name;
        userlogin.Email = model.Email;
        userlogin.Password = model.Password;
        await _db.UserLogins.AddAsync(userlogin);
        await _db.SaveChangesAsync();        

        return true;
    }

    public async Task<UserLogin?> GetUserEmail(string email)
    {
        UserLogin? userLogin = await _db.UserLogins.FirstOrDefaultAsync(e => e.Email == email);
        return userLogin;
    }

    public async Task<UserLogin?> GetUserId(int Id)
    {
        UserLogin? userLogin = await _db.UserLogins.FirstOrDefaultAsync(e => e.Id == Id);
        return userLogin;
    }
}
