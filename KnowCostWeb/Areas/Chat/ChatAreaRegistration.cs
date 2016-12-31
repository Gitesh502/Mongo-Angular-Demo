﻿using System.Web.Mvc;

namespace KnowCostWeb.Areas.Chat
{
    public class ChatAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Chat";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Chat_default",
                "Chat/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional ,AreaName="Chat"},
                new[] { "KnowCostWeb.Areas.Chat.Controllers" }
            );
        }
    }
}