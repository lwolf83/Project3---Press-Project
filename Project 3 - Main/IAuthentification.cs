using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public interface IAuthentification
    {
        bool LoginUsers(string login, string password);
    }
}