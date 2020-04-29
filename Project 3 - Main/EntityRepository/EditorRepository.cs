using System.Collections.Generic;
using System.Linq;
using Project_3___Press_Project.Entity;

namespace Project_3___Press_Project.Repository
{
    public class EditorRepository : RepositoryBase<Editor>
    {
        public override bool Exists(Editor entity)
        {
            throw new System.NotImplementedException();
        }

        public List<Editor> GetAllEditors()
        {
            List<Editor> editors;
            using (var context = new PressContext())
            {
                editors = (from i in context.Editors
                           select i).ToList();

            }
            return editors;
        }

    }
}
