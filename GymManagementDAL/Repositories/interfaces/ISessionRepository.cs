using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        IEnumerable<Session> GetAllSessionsWithTrainersAndCategories();
        int GetCountOfBookSlots(int sessionId);
        Session? GetSessionWithTrainerAndCategory(int sessionId);
    }
}
