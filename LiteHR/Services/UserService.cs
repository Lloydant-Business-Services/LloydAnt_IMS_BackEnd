using LiteHR.Dtos;
using LiteHR.Helpers;
using LiteHR.Infrastructure;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Services
{
    public class UserService : IUserService
    {
        private readonly HRContext _context;
        private readonly string defaultPassword = "1234567";
        private IEmailService _emailService;

        public UserService(HRContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<User> Authenticate(string username, string password)
        {

            //using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            //{
            //    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            //    for (int i = 0; i < computedHash.Length; i++)
            //    {
            //        if (computedHash[i] != storedHash[i]) return false;
            //    }
            //}

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _context.USER.Include(r => r.Role)
                .Include(r=>r.Staff)
                .SingleOrDefaultAsync(x => x.Username == username && x.Active);

            // check if username exists
            if (user == null)
                return null;
            

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public async Task<User> Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.USER.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.USER.Add(user);
            var x = await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task Delete(int id)
        {
            var user = _context.USER.Find(id);
            if (user != null)
            {
               _context.USER.Remove(user);
               await _context.SaveChangesAsync();
            }
            return;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.USER.Include(r => r.Role).ToListAsync();
        }

        public async Task<User> GetById(long id)
        {
            return await _context.USER.Include(r => r.Role).Include(r => r.Staff).ThenInclude(p => p.Person)
                .Where(u =>u.Id == id).FirstOrDefaultAsync();
        }

        public async Task ResetPassword(long userParam)
        {
            var user = await _context.USER.FindAsync(userParam);

            if (user == null)
                throw new AppException("User not found");

            if (userParam > 0)
            {
                // username has changed so check if the new username is already taken
                //if (await _context.USER.AnyAsync(x => x.Id == userParam))
                //    throw new AppException("Username " + userParam + " is already taken");
            }

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(defaultPassword))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(defaultPassword, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.USER.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User userParam, string password = null)
        {
            var user = await _context.USER.FindAsync(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (await _context.USER.AnyAsync(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken");
            }

            // update user properties
            user.Username = userParam.Username;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.USER.Update(user);
            await _context.SaveChangesAsync();
        }
        
    
        public async Task<ActionResult<int>> ChangePassword(long userId, ChangePassword changePasswordModel)
        {
            try
            {
                if (changePasswordModel == null)
                    throw new ArgumentNullException("Null Object");


                var user = await _context.USER.Where(p => p.Id == userId).FirstOrDefaultAsync();
                if (user == null)
                    throw new ArgumentNullException("Null Exception");
                byte[] passwordHash, passwordSalt;
                var isVerified = VerifyPasswordHash(changePasswordModel.OldPassword, user.PasswordHash, user.PasswordSalt);
                if (!isVerified)
                    throw new ArgumentNullException("Null Exception");

                CreatePasswordHash(changePasswordModel.NewPassword, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;


                _context.Update(user);
                await _context.SaveChangesAsync();

                return StatusCodes.Status200OK;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<int> ModifyUserPassword(ChangePassword dto)
        {
            try
            {
                if (dto == null)
                    throw new NullReferenceException("Null Object");


                var user = await _context.USER.Where(p => p.Id == dto.UserId).FirstOrDefaultAsync();
                if (user == null)
                    throw new NullReferenceException("Null Exception");
                byte[] passwordHash, passwordSalt;
                var isVerified = VerifyPasswordHash(dto.OldPassword, user.PasswordHash, user.PasswordSalt);
                if (!isVerified)
                    throw new NullReferenceException("Null Exception");

                CreatePasswordHash(dto.NewPassword, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;


                _context.Update(user);
                await _context.SaveChangesAsync();

                return StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> ResetPasswordRequest(string username)
        {
            //string Message;
            try
            {
                var vaiidateStaff = await _context.STAFF.Where(j => j.GeneratedStaffNumber == username).Include(p => p.Person).FirstOrDefaultAsync();

                if (vaiidateStaff == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    string guid = Convert.ToString(Guid.NewGuid());
                    vaiidateStaff.Guid = guid;
                    vaiidateStaff.DateOfLastReset = DateTime.Now;

                    _context.Update(vaiidateStaff);
                    await _context.SaveChangesAsync();

                    SendEmailDto sendEmailDto = new SendEmailDto()
                    {
                        VerificationCategory = (int)VerificationCategories.PasswordReset,
                        ReceiverEmail = vaiidateStaff.Person.Email,
                        ReceiverName = vaiidateStaff.Person.Firstname,
                        VerificationGuid = guid,
                        
                    };
                    await _emailService.EmailFormatter(sendEmailDto);

                    return StatusCodes.Status200OK;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<long> VerifyPasswordReset(string email, string guid)
        {
            var _staff = await _context.STAFF.Where(x => x.Guid == guid && x.Person.Email == email).FirstOrDefaultAsync();
            if(_staff != null)
            {
                return _staff.Id;
            }
            return 0;
        }

    }
}
