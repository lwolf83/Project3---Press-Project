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

        [SetUp]
        public void Setup()
        {
            Newspaper = new Newspaper();
            Newspaper.Name = "newspaperName";
            Newspaper.Periodicity = 15;
            Newspaper.Price = 5;

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
    }
}
