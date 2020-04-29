using System;
using System.Collections.Generic;
using System.Linq;
using Project_3___Press_Project.Entity;

namespace Project_3___Press_Project
{
    public static class CatalogLoader
    {
        public static IEnumerable<Catalog> Get(Newspaper newspaper)
        {
            IEnumerable<Catalog> catalogs;
            var context = new PressContext();
            catalogs = context.Catalogs.Where(c => c.Newspaper.NewspaperId == newspaper.NewspaperId).AsEnumerable();
            return catalogs;
        }

        public static List<Catalog> GetAll()
        {
            List<Editor> editors = new List<Editor>();
            List<Newspaper> newspapers = new List<Newspaper>();
            List<Catalog> catalogs = new List<Catalog>();
            using (var context = new PressContext())
            {
                editors = (from e in context.Editors
                           select e).ToList();
                newspapers = (from n in context.Newspapers
                              select n).ToList();
                catalogs = (from c in context.Catalogs
                            select c).ToList();
            }
            List<Catalog> catalogList = (from c in catalogs
                                         join n in newspapers on c.Newspaper.NewspaperId equals n.NewspaperId
                                         join e in editors on n.Editor.EditorId equals e.EditorId
                                         select c).Distinct().ToList();
            return catalogList;
        }

        public static List<Catalog> GetLatests(DateTime startingDate)
        {
            List<Catalog> allCatalogs = CatalogLoader.GetAll().ToList();
            List<Catalog> latestCatalogs = allCatalogs.Where(c => c.PublicationDate >= startingDate).OrderBy(c => c.PublicationDate).ToList();
            return latestCatalogs;
        }
    }
}
