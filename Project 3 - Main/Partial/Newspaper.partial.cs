using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_3___Press_Project
{
    public partial class Newspaper
    {
        public List<Newspaper> GetAllNewspapers()
        {
            List<Newspaper> newspapers = new List<Newspaper>();
            using (var context = new PressContext())
            {
                newspapers = (from e in context.Editors
                              join n in context.Newspapers on e.EditorId equals n.Editor.EditorId       // Ne récupère pas l'éditeur dans les newspapers...
                              orderby n.Name                                                            // Testé en mettant les listes en mémoire vive puis faire jointure, 
                              select n).ToList();                                                       // ca change rien
            }
            return newspapers;
        }
    }
}
