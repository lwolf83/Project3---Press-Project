using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project_3___Press_Project
{
    public static class EditorLoader
    {
        public static Editor Get(Editor editor)
        {
            using (var context = new PressContext())
            {
                var editorInDb = (from i in context.Editors
                              where i.EditorId == editor.EditorId
                              select i).First();
            }
            return editor;
        }
    }
}
