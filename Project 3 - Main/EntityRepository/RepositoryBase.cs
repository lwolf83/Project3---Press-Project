using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Project_3___Press_Project.Repository
{
    public abstract class RepositoryBase<IEntity> where IEntity : class
    {
        protected readonly PressContext _context;

        public RepositoryBase()
        {
            _context = new PressContext();
        }

        public abstract bool Exists(IEntity entity);

        public void Update(IEntity entity)
        {
            var entityTracking = _context.Entry(entity);
            if (entityTracking.Entity == null)
            {
                _context.Add(entity);
            }
            else
            {
                _context.Update(entity);
            }
        }

        public void Delete(IEntity entity)
        {
            _context.Remove(entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}