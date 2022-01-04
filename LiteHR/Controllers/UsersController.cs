using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiteHR.Models;
using LiteHR.Interface;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using LiteHR.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using LiteHR.Dtos;

namespace LiteHR.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;
        private readonly HRContext _context;


        public UsersController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(User model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null || user.Active == false)
                return BadRequest(new { errorFeed = StatusCodes.Status400BadRequest });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                RoleId = user.Role?.Id,
                Role = user.Role?.Name,
                Token = tokenString,
                StaffId=user.StaffId,
                PersonId = user.Staff?.PersonId,
                //FacultyId = user.Staff?.Department?.FacultyId,
                LoginStatus = StatusCodes.Status200OK

                
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(User model)
        {
           
            try
            {
                // create user
                await _userService.Create(model, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,User model)
        {
            // map model to entity and set id
            model.Id = id;

            try
            {
                // update user 
                await _userService.Update(model, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }




        //[AllowAnonymous]
        //[HttpPut("[action]")]
        //public async Task<ActionResult<int>> ChangePassword(long userId, ChangePassword changePasswordModel) => await _userService.ChangePassword(userId, changePasswordModel);

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<int>> ChangePassword(long userId, ChangePassword changePasswordModel) => await _userService.ChangePassword(userId, changePasswordModel);






        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task ResetPassword(long userParam) => await _userService.ResetPassword(userParam);

        [HttpPost("[action]")]
        public async Task<int> ResetPasswordRequest(string username) => await _userService.ResetPasswordRequest(username);

        [HttpGet("[action]")]
        public async Task<long> VerifyPasswordReset(string email, string guid) => await _userService.VerifyPasswordReset(email, guid);

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<int> ModifyUserPassword(ChangePassword dto) => await _userService.ModifyUserPassword(dto);


    }
}
