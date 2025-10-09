using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.ViewModels.PlanViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanViewModel? GetPlanById(int id);

        UpdatePlanViewModel? GetPlanToUpdate(int PlanId);
        bool UpdatePlan(int PlanId , UpdatePlanViewModel UpdatedPlan);

        bool ToggleStatus(int PlanId);
 
    
    
    }
}
