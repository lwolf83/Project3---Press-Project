using System.Collections.Generic;
using System.Linq;
using Project_3___Press_Project.Entity;

namespace Project_3___Press_Project
{
    public class JournalFilter
    {
        public IEnumerable<Newspaper> Newspaperlist { get; set; }

        public IEnumerable<Newspaper> DistinctEditors => Newspaperlist.GroupBy(x => x.Editor)
                                                                      .Select(x => x.FirstOrDefault());

        public IEnumerable<Newspaper> DistinctPeriodicity => Newspaperlist.GroupBy(x => x.Periodicity)
                                                                          .Select(x => x.FirstOrDefault());
        public JournalFilter()
        {
            using (var context = new PressContext())
            {
                Newspaperlist = (from np in context.Newspapers.AsEnumerable()
                                join ed in context.Editors.AsEnumerable() on np.Editor.EditorId equals ed.EditorId
                                orderby np.Name
                                select np).ToList();
            }
        }

        public JournalFilter(IEnumerable<Newspaper> newspapers)
        {
            Newspaperlist = newspapers;
        }

        public IEnumerable<Newspaper> GetNewspaper(Newspaper sEd = null, Newspaper sP = null)
        {
            var newspaper = Newspaperlist;       
            
            if ((sEd is null) && !(sP is null))
            {
                newspaper = Newspaperlist.Where(x => x.Periodicity == sP.Periodicity);
            }
            else if (!(sEd is null) && (sP is null))
            {
                newspaper = Newspaperlist.Where(x => x.Editor.Equals(sEd.Editor));
            }
            else if (!(sEd is null) && !(sP is null))
            {
                newspaper = Newspaperlist.Where(x => x.Editor.Equals(sEd.Editor) && x.Periodicity == sP.Periodicity);
            }
            
            return newspaper;
        }

        public IEnumerable<Newspaper> GetNewspaperEditor(Editor editor)
        {            
            return Newspaperlist.Where(x => x.Editor.EditorId.Equals(editor.EditorId));        
        }
    }
}