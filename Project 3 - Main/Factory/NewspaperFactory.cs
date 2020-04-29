using System.Collections.Generic;
using Project_3___Press_Project.Entity;

namespace Project_3___Press_Project.Factory
{
    public static class NewspaperFactory
    {
        public static Newspaper Create(string name, int periodiciy, string ean13, decimal price, List<Catalog> catalogs, Editor editor)
        {
            Newspaper newspaper = new Newspaper();
            newspaper.Name = name;
            newspaper.Periodicity = periodiciy;
            newspaper.EAN13 = ean13;
            newspaper.Price = price;
            newspaper.Catalogs = catalogs;
            newspaper.Editor = editor;

            return newspaper;
        }
    }
}
