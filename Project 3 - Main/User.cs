﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Function { get; set; }
        public ICollection<Shop> Shop { get; set; }
    }
}