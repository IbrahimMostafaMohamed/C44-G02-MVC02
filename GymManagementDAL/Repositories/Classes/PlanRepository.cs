using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Data;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.interfaces;

namespace GymManagementDAL.Repositories.Classes
{
    internal class PlanRepository : IPlanRepositories
    {
        private readonly GymDbContext _dbContext;

        public PlanRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Plan p)
        {
            _dbContext.Add(p);
            return _dbContext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var p = _dbContext.Plans.Find(Id);
            if (p == null)
                return 0;
            _dbContext.Plans.Remove(p);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Plan> GetAll() => _dbContext.Plans.ToList();


        public Plan? GetById(int Id)
        {
            return _dbContext.Plans.Find(Id);
        }

        public int Update(Plan p)
        {
            _dbContext.Plans.Update(p);
            return _dbContext.SaveChanges();
        }
    }
}
