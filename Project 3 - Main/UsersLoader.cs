using System.Linq;
using Project_3___Press_Project.Entity;

namespace Project_3___Press_Project
{
    public static class UsersLoader
    {
        public static User FromUserShop(UserShop us)
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
