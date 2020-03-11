using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    class DataCreation
    {
        public static void CreateData()
        {
            using (var context = new PressContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();


            }
        }
    }
}
