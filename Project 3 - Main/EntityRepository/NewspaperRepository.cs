using System;
using System.Collections.Generic;
using System.Linq;
using Project_3___Press_Project.Entity;

namespace Project_3___Press_Project.Repository
{
    public class NewspaperRepository : RepositoryBase<Newspaper>
    {
        public Newspaper FindByName(String name)
        {
            Newspaper newspaper = _context.Newspapers.SingleOrDefault(n => n.Name == name);
            return newspaper;
        }

        public Newspaper FindByEAN13(String ean13)
        {
            Newspaper newspaper = _context.Newspapers.SingleOrDefault(n => n.EAN13 == ean13);
            return newspaper;
        }

        public bool EAN13Exists(String ean13)
        {
            bool isRecorded = (from c in _context.Newspapers
                               where c.EAN13 == ean13
                               select c).Any();
            return isRecorded;
        }

        public IEnumerable<Newspaper> GetNewspaperData()
        {
            IEnumerable<Newspaper> newspapers = _context.Newspapers.AsEnumerable();
            IEnumerable<Editor> editors = _context.Editors.AsEnumerable();

            IEnumerable<Newspaper> dataNewspaper = from n in newspapers
                                                    join e in editors on n.Editor.EditorId equals e.EditorId
                                                    select n;
            return dataNewspaper.ToList();
        }

        public override bool Exists(Newspaper entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
