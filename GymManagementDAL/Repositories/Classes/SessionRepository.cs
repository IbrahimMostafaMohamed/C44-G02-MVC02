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
    internal class SessionRepository : ISessionRepositories
    {
        private readonly GymDbContext _dbContext = new GymDbContext();
        public int Add(Session s)
        {
            _dbContext.Add(s);
            return _dbContext.SaveChanges();
        }

        public int Delete(int Id)
        {
            var s = _dbContext.Sessions.Find(Id);
            if (s == null)
                return 0;
            _dbContext.Sessions.Remove(s);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Session> GetAll() => _dbContext.Sessions.ToList();


        public Session? GetById(int Id)
        {
            return _dbContext.Sessions.Find(Id);
        }

        public int Update(Session s)
        {
            _dbContext.Sessions.Update(s);
            return _dbContext.SaveChanges();
        }
    }
}
