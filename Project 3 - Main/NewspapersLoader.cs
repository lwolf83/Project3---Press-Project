using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_3___Press_Project
{
    public class NewspapersLoader
    {
        public static IEnumerable<Newspaper> GetAll()
        {
            var context = new PressContext();

            IEnumerable<Newspaper> newspapers = (from n in context.Newspapers
                                              join c in context.Catalogs
                                              on n.NewspaperId equals c.Newspaper.NewspaperId
                                              select n).ToList();
            return newspapers;
        }
    }
}
