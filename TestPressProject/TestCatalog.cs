using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Project_3___Press_Project.Entity;
using Project_3___Press_Project.Filter;

namespace TestPressProject
{
    public class TestCatalog
    {
        public Catalog Catalog { get; set; }
        public Newspaper Newspaper { get; set; }
        public List<Newspaper> Newspapers { get; set; }
        public List<Editor> Editors { get; set; }
        public List<Catalog> Catalogs { get; set; }

        [Test]
        public void TestFindCatalogByName()
        {
            var catalogs = new List<Catalog>
            {
                new Catalog { Name = "in" },
                new Catalog { Name = "out" }
            };

            CatalogFilter filter = new CatalogFilter(catalogs)
            {
                ["name"] = "in"
            };

            Assert.AreEqual(1, filter.Results.Count);
            Assert.IsTrue(filter.Results.Any(c => c == catalogs[0]));
        }

        /*
        [SetUp]
        public void Setup()
        {
            Editors = new List<Editor>();
            Newspapers = new List<Newspaper>();
            List<Newspaper> newspapers1 = new List<Newspaper>();
            List<Newspaper> newspapers2 = new List<Newspaper>();

            Editor editor1 = new Editor
            {
                Name = "Editor1",
                Newspapers = newspapers1,
                EditorId = Guid.NewGuid()
            };
            Editors.Add(editor1);

            Editor editor2 = new Editor
            {
                Name = "Editor2",
                Newspapers = newspapers2,
                EditorId = Guid.NewGuid()
            };
            Editors.Add(editor2);

            int counter = 0;
            foreach(Editor e in Editors)
            {
                for (int i = 0; i < 2; i++)
                {
                    Newspaper newspaper = new Newspaper
                    {
                        Name = $"Newspaper {e.Name}:{i}",
                        EAN13 = $"12345{counter}",
                        Periodicity = i,
                        Price = 5 + i,
                        Editor = e,
                        NewspaperId = Guid.NewGuid()
                    };
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
                    Catalog catalog = new Catalog
                    {
                        Name = $"Catalog {counter}",
                        Newspaper = np,
                        PublicationDate = DateTime.Today + TimeSpan.FromDays(i),
                        CatalogId = Guid.NewGuid()
                    };
                    tempCatalogs.Add(catalog);
                    counter = counter + 1;
                }
            }

            Catalogs = (from c in tempCatalogs
                            join n in Newspapers on c.Newspaper.NewspaperId equals n.NewspaperId
                            join e in Editors on n.Editor.EditorId equals e.EditorId
                            select c).ToList();

            Newspaper = new Newspaper
            {
                Name = "newspaperName",
                Periodicity = 15,
                Price = 5,
                Editor = editor1
            };

            Catalog = new Catalog
            {
                Name = "CatalogName",
                Newspaper = Newspaper,
                PublicationDate = DateTime.Today
            };
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

            var filter = new CatalogFilter(testCatalogs)
            {
                ["editor"] = Editors[0]
            };
            IEnumerable<Catalog> filteredCatalogs = filter.Results;

            Assert.AreEqual(testCatalogs, filteredCatalogs);
        }

        [Test]
        public void TestGetCatalogsFromEAN13()
        {
            List<Catalog> testCatalogs = new List<Catalog>() { Catalogs[0], Catalogs[1] };

            CatalogFilter filter = new CatalogFilter(testCatalogs)
            {
                ["EAN13"] = "123450"
            };

            Assert.AreEqual(testCatalogs, filter.Results);

        }

        [Test]
        public void GetCatalogsFromDates()
        {
            List<Catalog> testCatalogs = new List<Catalog>() { Catalogs[0], Catalogs[2], Catalogs[4], Catalogs[6] };

            var filter = new CatalogFilter(testCatalogs)
            {
                ["startDate"] = DateTime.Today,
                ["endDate"] = DateTime.Today
            };

            Assert.AreEqual(testCatalogs, filter.Results);
        }
        */
    }
}
