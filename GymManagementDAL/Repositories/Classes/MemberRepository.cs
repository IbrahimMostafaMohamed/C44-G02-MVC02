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
    internal class MemberRepository : IMemberRepositories
    {
        private readonly GymDbContext _dbContext;

        //private readonly GymDbContext _dbContext = new GymDbContext();
        public MemberRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public int Add(Member member)
        {
            _dbContext.Add(member);
            return _dbContext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var member = _dbContext.Members.Find(Id);
            if (member == null)
                return 0;
            _dbContext.Members.Remove(member);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Member> GetAll() => _dbContext.Members.ToList();


        public Member? GetById(int Id)
        {
            return _dbContext.Members.Find(Id);
        }

        public int Update(Member member)
        {
            _dbContext.Members.Update(member);
            return _dbContext.SaveChanges();
        }
    }
}
