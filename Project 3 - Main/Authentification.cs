using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public class Authentification : IAuthentification
    {

        public User LoginUsers(string login, string password)
        {
            String encryptedPassword = CryptPassword(password.ToString());

            using (var context = new PressContext())
            {
                return context.Users.Where(x => x.Login.Equals(login) && x.Password.Equals(encryptedPassword)).SingleOrDefault();
            }
        }
        private String CryptPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            SHA256 hasher = SHA256.Create();
            byte[] encryptedPasswordBytes = hasher.ComputeHash(passwordBytes);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < encryptedPasswordBytes.Length; i++)
            {
                builder.Append(encryptedPasswordBytes[i].ToString("x2"));
            }
            String encryptedPassword = builder.ToString();
            return encryptedPassword;
        }
    }
}
