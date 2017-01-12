﻿using KnowCostData.Entity;
using KnowCostData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KnowCostWeb.Api
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;
        //public UserController()
        //{
        //}
        public UserController(IUserRepository UserRepository)
        {
            this._userRepository = UserRepository;
        }
        [HttpGet]
        [Route("api/User/GetUserByEmail")]
        public users GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }
    }
}