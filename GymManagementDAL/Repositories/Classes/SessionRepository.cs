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
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbContext;

        public SessionRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Session> GetAllSessionsWithTrainersAndCategories()
        {
            return _dbContext.Sessions.Include(x=> x.Trainer)
                                      .Include(x=>x.Category)
                                      .ToList();
        }

        public int GetCountOfBookSlots(int sessionId)
        {
            return _dbContext.MemberSessions.Count(x => x.SessionId == sessionId);
        }

        public Session? GetSessionWithTrainerAndCategory(int sessionId)
        {
            return _dbContext.Sessions.Include(x => x.Trainer)
                                      .Include(x => x.Category)
                                      .FirstOrDefault(x => x.Id == sessionId);
        }

    
    }
}

