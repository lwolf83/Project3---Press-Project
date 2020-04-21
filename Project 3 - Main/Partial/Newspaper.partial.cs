using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public partial class Newspaper
    {
        public bool IsEAN13Registered(string ean13)
        {
            using (var context = new PressContext())
            {
                bool isRecorded = (from c in context.Newspapers
                                    where c.EAN13 == ean13
                                    select c).Any();
                return isRecorded;
            }
        }

        public bool DoesExist()
        {
            using (var context = new PressContext())
            {
                var isRecorded = (from n in context.Newspapers
                                  where n.Name == this.Name && n.EAN13 == this.EAN13 && n.Periodicity == this.Periodicity
                                  select n).Any();
                return isRecorded;
            }
        }

        public void Save()
        {
            using (var context = new PressContext())
            {
                context.Add(this);
                context.SaveChanges();
            }
        }

        public void SaveWithNewCatalog(Catalog catalog)
        {
            using (var context = new PressContext())
            {
                context.Update(catalog);
                context.Update(this);
                context.SaveChanges();
            }
        }
    }
}
