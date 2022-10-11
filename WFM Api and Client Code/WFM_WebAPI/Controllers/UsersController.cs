﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WFM_WebAPI.Helpers;
using WFM_WebAPI.Models;
using WFM_WebAPI.Services;

namespace WFM_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
  
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
    }
}
