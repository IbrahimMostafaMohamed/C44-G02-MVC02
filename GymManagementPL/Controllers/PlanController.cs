using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementBLL.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }
        public IActionResult Index()
        {
            var Plans = _planService.GetAllPlans();
            return View(Plans);
        }
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Plan Not be Zero Or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Plan = _planService.GetPlanById(id);
            if (Plan == null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Plan);
        }

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of plan Not be Zero Or Negative";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanToUpdate(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan Can Not be Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, UpdatePlanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Wrong Data", "Check Data Validation");
                return View(model);
            }
            bool Result = _planService.UpdatePlan(id,model);
            if (Result)
                TempData["SuccessMessage"] = "Plan Updated Successfully";
            else
                TempData["ErrorMessage"] = "Plan Failed To Update";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Activate(int id)
        {
            var Result = _planService.ToggleStatus(id);
            if (Result)
                TempData["SuccessMessage"] = "Plan Status changed";
            else
                TempData["ErrorMessage"] = "Failed To change Plan Status ";
            return RedirectToAction(nameof(Index));

        }



    }
}
