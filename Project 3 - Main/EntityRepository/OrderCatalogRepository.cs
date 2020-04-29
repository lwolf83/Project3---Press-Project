using System.Linq;
using Project_3___Press_Project.Entity;

namespace Project_3___Press_Project.Repository
{
    public class OrderCatalogRepository : RepositoryBase<OrderCatalog>
    {
        public override bool Exists(OrderCatalog entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(Order order, Catalog catalog)
        {
            throw new System.NotImplementedException();
        }
    }
}
