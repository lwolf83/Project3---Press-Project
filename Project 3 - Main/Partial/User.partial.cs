﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public partial class User
    {
        public static User GetUserFromUserShop(UserShop us)
        {
            User user = null;
            using (var context = new PressContext())
            {
                user = (from u in context.Users
                        where u.UserId == us.UserId
                        select u).First();
            }
            return user;
        }

    }
}
