using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Project_3___Press_Project.Entity;

namespace Project_3___Press_Project.Repository
{
    public class CatalogRepository : RepositoryBase<Catalog>
    {
        public List<Editor> GetEditorsHavingCatalogs(List<Catalog> catalogs)
        {
            var editors = (from e in catalogs
                           orderby e.Newspaper.Editor.Name
                           select e.Newspaper.Editor).Distinct();
            return editors.ToList();
        }

        public List<Catalog> GetCatalogsFromAnEditor(List<Catalog> catalogs, Editor selectedEditor)
        {
            List<Catalog> selectedCatalogs = (from c in catalogs
                                              where c.Newspaper.Editor.EditorId == selectedEditor.EditorId
                                              select c).Distinct().ToList();
            return selectedCatalogs;
        }

        public List<Newspaper> GetNewspapersHavingCatalogs(List<Catalog> catalogs)
        {
            var newspapers = (from c in catalogs
                              orderby c.Newspaper.Name
                              select c.Newspaper).Distinct();
            return newspapers.ToList();
        }

        public List<Catalog> GetCatalogsFromANewspaper(List<Catalog> catalogs, Newspaper selectedNewspaper)
        {
            List<Catalog> selectedCatalogs = (from c in catalogs
                                              where c.Newspaper.NewspaperId == selectedNewspaper.NewspaperId
                                              select c).Distinct().ToList();
            return selectedCatalogs;
        }

        public List<Catalog> GetCatalogsFromDates(List<Catalog> catalogs, DateTime firstDate, DateTime lastDate)
        {
            List<Catalog> selectedCatalogs = (from c in catalogs
                                              where c.PublicationDate >= firstDate && c.PublicationDate <= lastDate
                                              select c).Distinct().ToList();
            return selectedCatalogs;
        }

        public List<Catalog> GetCatalogsFromEAN13(List<Catalog> catalogs, string ean13)
        {
            List<Catalog> selectedCatalogs = (from c in catalogs
                                              where c.Newspaper.EAN13 == ean13
                                              select c).Distinct().ToList();
            return selectedCatalogs;
        }

        public override bool Exists(Catalog entity)
        {
            throw new NotImplementedException();
        }
    }
}