using System;
using System.Collections.Generic;
using System.Text;
using Project_3___Press_Project;
using System.Linq;
using NUnit.Framework;

namespace TestPressProject
{
    public class TestCatalog
    {
        public Catalog Catalog { get; set; }
        public Newspaper Newspaper { get; set; }
        public List<Newspaper> Newspapers { get; set; }
        public List<Editor> Editors { get; set; }
        public List<Catalog> Catalogs { get; set; }

        [SetUp]
        public void Setup()
        {
            Editors = new List<Editor>();
            Newspapers = new List<Newspaper>();
            List<Newspaper> newspapers1 = new List<Newspaper>();
            List<Newspaper> newspapers2 = new List<Newspaper>();

            Editor editor1 = new Editor();
            editor1.Name = "Editor1";
            editor1.Newspapers = newspapers1;
            editor1.EditorId = Guid.NewGuid();
            Editors.Add(editor1);

            Editor editor2 = new Editor();
            editor2.Name = "Editor2";
            editor2.Newspapers = newspapers2;
            editor2.EditorId = Guid.NewGuid();
            Editors.Add(editor2);

            int counter = 0;
            foreach(Editor e in Editors)
            {
                for (int i = 0; i < 2; i++)
                {
                    Newspaper newspaper = new Newspaper();
                    newspaper.Name = $"Newspaper {e.Name}:{i}";
                    newspaper.EAN13 = $"12345{counter}";
                    newspaper.Periodicity = i;
                    newspaper.Price = 5+i;
                    newspaper.Editor = e;
                    newspaper.NewspaperId = Guid.NewGuid();
                    Newspapers.Add(newspaper);
                    counter = counter + 1;
                }
            }
            newspapers1.Add(Newspapers[0]);
            newspapers1.Add(Newspapers[1]);
            newspapers2.Add(Newspapers[2]);
            newspapers2.Add(Newspapers[3]);

            counter = 0;
            List<Catalog> tempCatalogs = new List<Catalog>();
            foreach (Newspaper np in Newspapers)
            {
                for (int i = 0; i < 2; i++)
                {
                    Catalog catalog = new Catalog();
                    catalog.Name = $"Catalog {counter}";
                    catalog.Newspaper = np;
                    catalog.PublicationDate = DateTime.Today + TimeSpan.FromDays(i);
                    catalog.CatalogId = Guid.NewGuid();
                    tempCatalogs.Add(catalog);
                    counter = counter + 1;
                }
            }

            Catalogs = (from c in tempCatalogs
                            join n in Newspapers on c.Newspaper.NewspaperId equals n.NewspaperId
                            join e in Editors on n.Editor.EditorId equals e.EditorId
                            select c).ToList();

            Newspaper = new Newspaper();
            Newspaper.Name = "newspaperName";
            Newspaper.Periodicity = 15;
            Newspaper.Price = 5;
            Newspaper.Editor = editor1;

            Catalog = new Catalog();
            Catalog.Name = "CatalogName";
            Catalog.Newspaper = Newspaper;
            Catalog.PublicationDate = DateTime.Today;
        }

        [Test]
        public void TestCatalogFactory()
        {
            string catalogName = "CatalogName";
            DateTime catalogPublicationDate = DateTime.Today;
            Catalog factoredCatalog = CatalogFactory.Create(catalogName, catalogPublicationDate, Newspaper);
            MultiplePropertiesAssertion.AssertAreEqual(Catalog, factoredCatalog);
        }

        [Test]
        public void TestGetCatalogsFromAnEditor()
        {
            List<Catalog> testCatalogs = new List<Catalog>() { Catalogs[0], Catalogs[1], Catalogs[2], Catalogs[3] };
            List<Catalog> filteredCatalogs = Catalog.GetCatalogsFromAnEditor(Catalogs, Editors[0]);
            Assert.AreEqual(testCatalogs, filteredCatalogs);
        }

        [Test]
        public void TestGetCatalogsFromEAN13()
        {
            List<Catalog> testCatalogs = new List<Catalog>() { Catalogs[0], Catalogs[1] };
            List<Catalog> filteredCatalogs = Catalog.GetCatalogsFromEAN13(Catalogs, "123450");
            Assert.AreEqual(testCatalogs, filteredCatalogs);
        }

        [Test]
        public void GetCatalogsFromDates()
        {
            List<Catalog> testCatalogs = new List<Catalog>() { Catalogs[0], Catalogs[2], Catalogs[4], Catalogs[6] };
            List<Catalog> filteredCatalogs = Catalog.GetCatalogsFromDates(Catalogs, DateTime.Today, DateTime.Today);
            Assert.AreEqual(testCatalogs, filteredCatalogs);
        }

        [Test]
        public void GetCatalogsFromNewspaper()
        {
            List<Catalog> testCatalogs = new List<Catalog>() { Catalogs[0], Catalogs[1] };
            List<Catalog> filteredCatalogs = Catalog.GetCatalogsFromANewspaper(Catalogs, Newspapers[0]);
        }
    }
}
