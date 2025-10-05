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
    internal class MembershipRepository : IMembershipRepositories
    {

        private readonly GymDbContext _dbContext;

        public MembershipRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Membership member)
        {
            _dbContext.Add(member);
            return _dbContext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var member = _dbContext.Memberships.Find(Id);
            if (member == null)
                return 0;
            _dbContext.Memberships.Remove(member);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Membership> GetAll() => _dbContext.Memberships.ToList();


        public Membership? GetById(int Id)
        {
            return _dbContext.Memberships.Find(Id);
        }

        public int Update(Membership member)
        {
            _dbContext.Memberships.Update(member);
            return _dbContext.SaveChanges();
        }
    }
}
