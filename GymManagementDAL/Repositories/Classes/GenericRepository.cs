using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Data;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly GymDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public IEnumerable<T> GetAll(Func<T, bool>? condition = null)
        {
            if (condition == null)
                return _dbSet.AsNoTracking().ToList();
            else
                return _dbSet.AsNoTracking().Where(condition).ToList();
        }

        public T? GetById(int Id) => _dbSet.Find(Id);


        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
