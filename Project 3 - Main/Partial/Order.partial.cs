

namespace Project_3___Press_Project
{
    public partial class Order
    {
        public void SaveInDb(OrderCatalog orderCatalog)
        {
            using (var context = new PressContext())
            {
                context.Update(this);
                context.Add(orderCatalog);
                context.SaveChanges();
            }
        }

    }
}
