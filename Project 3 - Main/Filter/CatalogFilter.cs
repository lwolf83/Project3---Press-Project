using Project_3___Press_Project.Entity;
using Project_3___Press_Project.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Project_3___Press_Project.Filter
{
    public class CatalogFilter : FilterBase<Catalog>
    {
        public CatalogFilter(ICollection<Catalog> entities) : base(entities)
        { }
    }
}
