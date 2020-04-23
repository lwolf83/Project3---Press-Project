using System.Collections.Generic;
using System.Linq;

namespace Project_3___Press_Project
{
    public partial class Editor
    {
        public List<Editor> GetAllEditors()
        {
            List<Editor> editors = new List<Editor>();
            using (var context = new PressContext())
            {
                editors = (from i in context.Editors
                           select i).ToList();

            }
            return editors;
        }

    }
}
