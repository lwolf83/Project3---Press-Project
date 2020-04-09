using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public partial class Catalog
    {
        public void DeleteInDB()
        {
            using (var context = new PressContext())
            {
                context.Catalogs.Remove(this);
                context.SaveChanges();
            }
        }

        public void SaveInDB()
        {
            using (var context = new PressContext())
            {
                context.Catalogs.Update(this);
                context.SaveChanges();
            }
        }
    }
}
