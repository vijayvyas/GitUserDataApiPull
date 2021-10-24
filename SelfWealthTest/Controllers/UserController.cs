﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelfWealthTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUserApiService _userApiService;

        public UserController(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        [HttpPost]
        public async Task<List<UsersDto>> RetrieveUsers([FromBody] List<string> users)
        {
            List<UsersDto> userDetails = await _userApiService.ReteriveUsers(users);
            return userDetails;
        }
    }
}
