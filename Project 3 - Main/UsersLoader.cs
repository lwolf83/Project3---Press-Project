using System.Linq;

namespace Project_3___Press_Project
{
    public class UsersLoader
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
