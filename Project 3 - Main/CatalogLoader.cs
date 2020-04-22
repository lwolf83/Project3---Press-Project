using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_3___Press_Project
{
    public class CatalogLoader
    {
        public static IEnumerable<Catalog> Get(Newspaper newspaper)
        {
            IEnumerable<Catalog> catalogs;
            var context = new PressContext();
            catalogs = context.Catalogs.Where(c => c.Newspaper.NewspaperId == newspaper.NewspaperId).AsEnumerable();
            return catalogs;
        }
    }
}
