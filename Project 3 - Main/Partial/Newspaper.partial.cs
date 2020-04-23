using System.Collections.Generic;
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

        public bool Exists()
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

        public static IEnumerable<Newspaper> GetNewspaperData()
        {
            using (var context = new PressContext())
            {
                IEnumerable<Newspaper> newspapers = context.Newspapers.AsEnumerable();
                IEnumerable<Editor> editors = context.Editors.AsEnumerable();

                IEnumerable<Newspaper> dataNewspaper = from n in newspapers
                                                       join e in editors on n.Editor.EditorId equals e.EditorId
                                                       select n;
                return dataNewspaper.ToList();
            }
        }

        public static bool ModifyNewspaper(string EAN13, string name, decimal price, int period)
        {
            using (var context = new PressContext())
            {
                bool exists = context.Newspapers.Where(n => n.EAN13.Equals(EAN13)).Any();
                if (exists)
                {
                    var np = context.Newspapers.Where(n => n.EAN13.Equals(EAN13)).First();
                    np.Name = name;
                    np.Price = price;
                    np.Periodicity = period;
                    context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
