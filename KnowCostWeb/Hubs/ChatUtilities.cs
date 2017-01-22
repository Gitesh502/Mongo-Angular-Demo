using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KnowCostWeb.ChatUtilities
{

    public class UserDetail
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }

    public class MessageDetail
    {

        public string UserName { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string ConnectionId { get; set; }
        public string NickName { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }

    public class PrivateChatMessage
    {
        public string userName { get; set; }
        public string message { get; set; }
    }

    public class ConnectedUsers
    {
        public ConnectedUsers()
        {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this))
            {
                DefaultValueAttribute myAttribute = (DefaultValueAttribute)property.Attributes[typeof(DefaultValueAttribute)];

                if (myAttribute != null)
                {
                    property.SetValue(this, myAttribute.Value);
                }
            }
        }
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public bool IsPriavteBoxOpened { get; set; }

    }

}