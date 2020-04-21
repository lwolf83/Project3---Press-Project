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
        public Editor Editor1 { get; set; }
        public List<Newspaper> Newspapers1 { get; set; }
        public Editor Editor2 { get; set; }
        public List<Newspaper> Newspapers2 { get; set; }
        public List<Newspaper> Newspapers { get; set; }

        public List<Editor> Editors { get; set; }
        public List<Catalog> Catalogs { get; set; }

        public List<Catalog> TestCatalogs { get; set; }


        [SetUp]
        public void Setup()
        {
            Editors = new List<Editor>();
            Newspapers = new List<Newspaper>();
            Newspapers1 = new List<Newspaper>();
            Newspapers2 = new List<Newspaper>();
            Catalogs = new List<Catalog>();

            Editor1 = new Editor();
            Editor1.Name = "Editor1";
            Editor1.Newspapers = Newspapers1;
            Editor1.EditorId = Guid.NewGuid();
            Editors.Add(Editor1);

            Editor2 = new Editor();
            Editor2.Name = "Editor2";
            Editor2.Newspapers = Newspapers2;
            Editor2.EditorId = Guid.NewGuid();
            Editors.Add(Editor2);

            foreach(Editor e in Editors)
            {
                for (int i = 0; i < 2; i++)
                {
                    Newspaper newspaper = new Newspaper();
                    newspaper.Name = $"Newspaper {e.Name}:{i}";
                    newspaper.Periodicity = i;
                    newspaper.Price = 5+i;
                    newspaper.Editor = e;
                    newspaper.NewspaperId = Guid.NewGuid();
                    Newspapers.Add(newspaper);
                }
            }
            Newspapers1.Add(Newspapers[0]);
            Newspapers1.Add(Newspapers[1]);
            Newspapers2.Add(Newspapers[2]);
            Newspapers2.Add(Newspapers[3]);

            foreach(Newspaper np in Newspapers)
            {
                for (int i = 0; i < 2; i++)
                {
                    Catalog catalog = new Catalog();
                    Catalog = new Catalog();
                    Catalog.Name = $"Catalog {np.Name}:{i}";
                    Catalog.Newspaper = np;
                    Catalog.PublicationDate = DateTime.Today + TimeSpan.FromDays(i);
                    Catalog.CatalogId = Guid.NewGuid();
                    Catalogs.Add(catalog);
                }
            }

            Newspaper = new Newspaper();
            Newspaper.Name = "newspaperName";
            Newspaper.Periodicity = 15;
            Newspaper.Price = 5;
            Newspaper.Editor = Editor1;

            Catalog = new Catalog();
            Catalog.Name = "CatalogName";
            Catalog.Newspaper = Newspaper;
            Catalog.PublicationDate = DateTime.Today;

            TestCatalogs = (from c in Catalogs
                        join n in Newspapers on c.Newspaper.NewspaperId equals n.NewspaperId
                        join e in Editors on n.Editor.EditorId equals e.EditorId
                        select c).ToList();
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
            List<Catalog> testCatalogs = new List<Catalog>() { TestCatalogs[0], TestCatalogs[1], TestCatalogs[2], TestCatalogs[3] };
            List<Catalog> selectedCatalogs = Catalog.GetCatalogsFromAnEditor(TestCatalogs, Editor1);
            Assert.AreEqual(testCatalogs, selectedCatalogs);
        }
    }
}
