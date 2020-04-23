using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_3___Press_Project
{
    public class EditorAddModify
    {
        private PressContext context;
        public List<Editor> Editors { get; set; } = new List<Editor>();

        public EditorAddModify()
        {
            context = new PressContext();
            Editors = context.Editors.AsEnumerable().ToList();
        }

        public bool CheckEditor(string name)
        {
            bool editorExist = Editors.Where(n => n.Name.Equals(name)).Any();

            if (editorExist)
            {
                return true;
            }
            else
            {
                AddEditor(name);
                return false;
            }
        }

        public void AddEditor(string name)
        {
            Editor editor = new Editor();
            editor.Name = name;
            context.Add(editor);
            context.SaveChanges();
        }
        public void ModifyEditor(Editor editor, string name)
        {
            editor.Name = name;
            context.SaveChanges();
        }
    } 
}