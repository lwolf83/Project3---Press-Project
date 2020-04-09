using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Project_3___Press_Project
{
    public partial class Catalog
    {
        public List<Catalog> GetAllCatalogs()
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
                if(this.CatalogId == Guid.Empty)
                {
                    context.Entry(Newspaper).State = EntityState.Unchanged;
                    context.Catalogs.Add(this);
                }
                else
                {
                    context.Catalogs.Update(this);
                }
                context.SaveChanges();
            }
        }
    }
}