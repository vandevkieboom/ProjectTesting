﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTesting
{
    public class UserService : IUserService
    {
        private string userUrl;
        public string UserUrl
        {
            get { return userUrl; }
            set { userUrl = value; }
        }

        public User GetUser(int id)
        {
            //hier komt dan de code voor het ophalen van een gebruiker
            throw new NotImplementedException();
        }
    }
}
