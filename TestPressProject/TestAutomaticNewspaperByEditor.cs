using System;
using System.Collections.Generic;
using System.Text;
using Project_3___Press_Project;
using System.Linq;
using NUnit.Framework;


namespace TestPressProject
{
    public class TestAutomaticNewspaperFilter
    {
        public List<Newspaper> Newspapers { get; set; }
        JournalFilter journalFilter;

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
            for(int i = 0; i < 16; i++)
            {
                Newspaper currentNewspaper = new Newspaper();
                currentNewspaper.Editor = editors[i%3];
                currentNewspaper.Periodicity = i % 2 == 0 ? 3 : 7;
                Newspapers.Add(currentNewspaper);
            }
            
            journalFilter = new JournalFilter(Newspapers);
        }

        [Test]
        public void FilteringNewspapersByEdition()
        {
            List<Newspaper> resComputed = journalFilter.GetNewspaper(Newspapers[0]).ToList();
            List<Newspaper> resExpected = new List<Newspaper>();
            resExpected.Add(Newspapers[0]);
            resExpected.Add(Newspapers[3]);
            resExpected.Add(Newspapers[6]);
            resExpected.Add(Newspapers[9]);
            resExpected.Add(Newspapers[12]);
            resExpected.Add(Newspapers[15]);
            Assert.AreEqual(resComputed, resExpected);
        }

        [Test]
        public void FilteringNewspapersNoFilter()
        {
            List<Newspaper> resComputed = journalFilter.GetNewspaper().ToList();
            Assert.AreEqual(resComputed, Newspapers);
        }

        [Test]
        public void FilteringNewspapersByEditionKO()
        {
            Newspaper currentNewspaper = new Newspaper();
            currentNewspaper.Editor = new Editor();
            List<Newspaper> resComputed = journalFilter.GetNewspaper(currentNewspaper).ToList();
            Assert.AreEqual(resComputed, new List<Newspaper>());
        }

        [Test]
        public void FilteringNewspapersByPeriodicity()
        {
            List<Newspaper> resComputed = journalFilter.GetNewspaper(null, Newspapers[1]).ToList();
            List<Newspaper> resExpected = new List<Newspaper>();
            resExpected.Add(Newspapers[1]);
            resExpected.Add(Newspapers[3]);
            resExpected.Add(Newspapers[5]);
            resExpected.Add(Newspapers[7]);
            resExpected.Add(Newspapers[9]);
            resExpected.Add(Newspapers[11]);
            resExpected.Add(Newspapers[13]);
            resExpected.Add(Newspapers[15]);
            Assert.AreEqual(resComputed, resExpected);
        }

        [Test]
        public void FilteringNewspapersByPeriodicityKO()
        {
            Newspaper dummyNewspaper = new Newspaper();
            dummyNewspaper.Periodicity = 5;
            List<Newspaper> resComputed = journalFilter.GetNewspaper(null, dummyNewspaper).ToList();
            Assert.AreEqual(resComputed, new List<Newspaper>());
        }

        [Test]
        public void FilteringNewspapersByEditorAndPeriodicity()
        {
            List<Newspaper> resComputed = journalFilter.GetNewspaper(Newspapers[0], Newspapers[1]).ToList();
            List<Newspaper> resExpected = new List<Newspaper>();
            resExpected.Add(Newspapers[3]);
            resExpected.Add(Newspapers[9]);
            resExpected.Add(Newspapers[15]);
            Assert.AreEqual(resComputed, resExpected);
        }

        [Test]
        public void FilteringNewspapersByEditorAndPeriodicityKO()
        {
            Newspaper dummyNewspaper = new Newspaper();
            dummyNewspaper.Editor = new Editor();
            dummyNewspaper.Periodicity = 5;
            List<Newspaper> resComputed = journalFilter.GetNewspaper(dummyNewspaper, dummyNewspaper).ToList();
            Assert.AreEqual(resComputed, new List<Newspaper>());
        }
    }
}
