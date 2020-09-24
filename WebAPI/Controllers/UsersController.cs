using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublishingCompany.Domain;
using PublishingCompany.Repository;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService { get; set; }

        public UserController(UserService userService)
        {
            this._userService = userService;

        }


        [Route("Token")]
        [HttpPost]
        [RequireHttps]
        public IActionResult Token([FromBody] LoginRequest loginRequest)
        {
            var token = this._userService.Login(loginRequest.Email, loginRequest.Password);

            if (String.IsNullOrWhiteSpace(token))
            {
                return BadRequest("Email or password invalid!");
            }

            return Ok(new
            {
                Token = token
            });
        }

    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
