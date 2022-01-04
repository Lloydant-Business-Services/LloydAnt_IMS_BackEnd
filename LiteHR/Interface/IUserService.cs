using LiteHR.Dtos;
using LiteHR.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<List<User>> GetAll();
        Task<User> GetById(long id);
        Task<User> Create(User user, string password);
        Task Update(User user, string password = null);
        Task ResetPassword(long user);
        Task Delete(int id);
        Task<ActionResult<int>> ChangePassword(long userId, ChangePassword changePasswordModel);
        Task<int> ResetPasswordRequest(string username);
        Task<long> VerifyPasswordReset(string email, string guid);
        Task<int> ModifyUserPassword(ChangePassword dto);


    }
}
