using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using codestackdemo.Entities;
using codestackdemo.Extensions;
using codestackdemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace codestackdemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public List<User> GetUsers()
        {
            HttpContext.Response.Headers.TryGetValue("Authorization", out var auth);
            auth = auth.ToString().Replace("Bearer ", "");

            var token = new JwtSecurityTokenHandler().ReadJwtToken(auth).Claims.Where(x => x.Type == "UserId").FirstOrDefault();

            var realId = Hash.GetValue(token.ToString());

            return _service.GetUsers();
        }

        [Authorize(Roles = "1, 2, 4")]
        [HttpPost]
        public void AddUser([FromBody] User user)
        {
            _service.AddUser(user);
        }
    }
}