using System;
using System.Collections.Generic;
using System.Text;
using Project_3___Press_Project;
using System.Linq;
using NUnit.Framework;


namespace TestPressProject
{
    public class TestAutomaticNewspaperByEditor
    {
        public List<Newspaper> Newspapers { get; set; }
       
        [SetUp]
        public void Setup()
        {
            List<Editor> editors = new List<Editor>();
            for(int i = 0; i < 3; i++)
            {
                Editor currentEditor = new Editor();
                editors.Add(currentEditor);
            }

            Newspapers = new List<Newspaper>();
            for(int i = 0; i < 10; i++)
            {
                Newspaper currentNewspaper = new Newspaper();
                currentNewspaper.Editor = editors[i%3];
                Newspapers.Add(currentNewspaper);
            }
        }

        [Test]
        public void FilteringNewspapersByEdition()
        {
            JournalFilter journalFilter = new JournalFilter(Newspapers);
            List<Newspaper> resComputed = journalFilter.GetNewspaper(Newspapers[0]).ToList();
            List<Newspaper> resExpected = new List<Newspaper>();
            resExpected.Add(Newspapers[0]);
            resExpected.Add(Newspapers[3]);
            resExpected.Add(Newspapers[6]);
            resExpected.Add(Newspapers[9]);
            Assert.AreEqual(resComputed, resExpected);
        }

        [Test]
        public void FilteringNewspapersByEditionNoFilter()
        {
            JournalFilter journalFilter = new JournalFilter(Newspapers);
            List<Newspaper> resComputed = journalFilter.GetNewspaper().ToList();
            Assert.AreEqual(resComputed, Newspapers);
        }

        [Test]
        public void FilteringNewspapersByEditionKO()
        {
            Newspaper currentNewspaper = new Newspaper();
            currentNewspaper.Editor = new Editor();
            JournalFilter journalFilter = new JournalFilter(Newspapers);
            List<Newspaper> resComputed = journalFilter.GetNewspaper(currentNewspaper).ToList();
            Assert.AreEqual(resComputed, new List<Newspaper>());
        }

    }
}
