using System.Linq;

namespace Project_3___Press_Project
{
    public static class EditorLoader
    {
        public static Editor Get(Editor editor)
        {
            Editor editorInDb;
            using (var context = new PressContext())
            {
                editorInDb = (from i in context.Editors
                              where i.EditorId == editor.EditorId
                              select i).First();
            }
            return editorInDb;
        }
    }
}
