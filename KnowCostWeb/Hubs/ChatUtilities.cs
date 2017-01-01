﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowCostWeb.ChatUtilities
{
   
    public class UserDetail
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
    }

    public class MessageDetail
    {

        public string UserName { get; set; }

        public string Message { get; set; }

    }
}