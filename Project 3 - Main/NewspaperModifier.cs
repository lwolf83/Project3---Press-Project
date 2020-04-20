using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public class NewspaperModifier
    {
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
