using System;
using System.Collections.Generic;
using System.Text;

namespace Project_3___Press_Project
{
    public class CatalogFactory
    {
        public static Catalog Create(string name, DateTime publicationDate, Newspaper newspaper)
        {
            Catalog catalog;
            catalog = new Catalog();
            catalog.Newspaper = newspaper;
            catalog.Name = name;
            catalog.PublicationDate = publicationDate;
            return catalog;
        }
    }
}
