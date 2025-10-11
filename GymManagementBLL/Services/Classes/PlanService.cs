using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.interfaces;

namespace GymManagementBLL.Services.Classes
{
    internal class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (Plans == null || !Plans.Any()) return [];
            return Plans.Select(P => new PlanViewModel()
            {
                Id = P.Id,
                Name = P.Name,
                Description = P.Description,
                DurationDays = P.DurationDays,
                IsActive = P.IsActive,
                Price = P.Price
            });
        }

        public PlanViewModel? GetPlanById(int id)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if (Plan == null) return null;
            return new PlanViewModel()
            {
                Id = Plan.Id,
                Name = Plan.Name,
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                IsActive = Plan.IsActive,
                Price = Plan.Price
            };
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int PlanId)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan == null || Plan.IsActive == false || HasActiveMemberShips(PlanId)) return null;
            return new UpdatePlanViewModel()
            {
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                PlanName = Plan.Name,
                Price = Plan.Price
            };
        }

        public bool UpdatePlan(int PlanId, UpdatePlanViewModel UpdatedPlan)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan is null || HasActiveMemberShips(PlanId)) return false;

            try
            {
                (Plan.Description, Plan.Price, Plan.DurationDays, Plan.UpdatedAt)
                    = (UpdatedPlan.Description, UpdatedPlan.Price, UpdatedPlan.DurationDays, DateTime.Now);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }

        public bool ToggleStatus(int PlanId)
        {
            var Repo = _unitOfWork.GetRepository<Plan>();
            var Plan = Repo.GetById(PlanId);
            if (Plan is null || HasActiveMemberShips(PlanId)) return false;
            Plan.IsActive = Plan.IsActive ? false : true;
            Plan.UpdatedAt = DateTime.Now;
            try
            {
                Repo.Update(Plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }

        #region Helper

        private bool HasActiveMemberShips(int PlanId)
        {
            var ActiveMemberShips = _unitOfWork.GetRepository<Membership>()
                        .GetAll(x => x.PlanId == PlanId && x.Status == "Active");
            return ActiveMemberShips.Any();

        }

        #endregion

    }
}
