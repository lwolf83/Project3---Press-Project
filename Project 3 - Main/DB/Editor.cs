using System;
using System.Collections.Generic;

namespace Project_3___Press_Project
{
    public partial class Editor
    {
        public Guid EditorId { get; set; }
        public string Name { get; set; }
        public ICollection<Newspaper> Newspapers { get; set; }
    }
}