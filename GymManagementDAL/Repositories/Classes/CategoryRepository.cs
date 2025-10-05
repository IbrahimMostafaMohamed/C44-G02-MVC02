using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Data;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.interfaces;

namespace GymManagementDAL.Repositories.Classes
{
    internal class CategoryRepository : ICategoryRepositories
    {
        private readonly GymDbContext _dbContext = new GymDbContext();
        public int Add(Category c)
        {
            _dbContext.Add(c);
            return _dbContext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var c = _dbContext.Categories.Find(Id);
            if (c == null)
                return 0;
            _dbContext.Categories.Remove(c);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Category> GetAll() => _dbContext.Categories.ToList();


        public Category? GetById(int Id)
        {
            return _dbContext.Categories.Find(Id);
        }

        public int Update(Category c)
        {
            _dbContext.Categories.Update(c);
            return _dbContext.SaveChanges();
        }
    }
}
