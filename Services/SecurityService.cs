﻿using AuctionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApplication.Services
{
    public class SecurityService
    {
        UserDAO userDAO = new UserDAO();

        public SecurityService()
        {
            
        }

        public bool IsValid(UserModel user)
        {
            return userDAO.FindUserByLoginAndPassword(user);
        }

        public bool Exists(UserModel user)
        {
            return userDAO.FindUserByLogin(user);
        }

        public void AddUser(UserModel user)
        {
            userDAO.AddNewUser(user);
        }

        public int GetId(UserModel user)
        {
            return userDAO.GetUserId(user);
        }
    }
}
