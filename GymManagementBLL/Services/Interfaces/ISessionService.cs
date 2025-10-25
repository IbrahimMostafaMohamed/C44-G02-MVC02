using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionById(int id);
        bool CreatSession(CreateSessionViewModel creeatSession);
        UpdateSessionViewModel? GetSessionToUpdate(int SessionId);
        bool UpdateSession (UpdateSessionViewModel UpdateSession , int SessionId);
        bool RemoveSession(int SessionId);
        IEnumerable<TrainerSelectViewModel> GetTrainersForDropdown();
        IEnumerable<CategorySelectViewModel> GetCategoryForDropdown();
        
    }
}
