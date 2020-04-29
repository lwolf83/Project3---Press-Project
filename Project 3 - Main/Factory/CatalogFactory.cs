using System;
using Project_3___Press_Project.Entity;

namespace Project_3___Press_Project.Factory
{
    public static class CatalogFactory
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
