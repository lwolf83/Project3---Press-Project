using Project_3___Press_Project.Entity;
using System.Collections.Generic;

namespace Project_3___Press_Project.Filter
{
    class NewspaperFilter : FilterBase<Newspaper>
    {
        public NewspaperFilter(ICollection<Newspaper> entities) : base(entities)
        { }
    }
}
