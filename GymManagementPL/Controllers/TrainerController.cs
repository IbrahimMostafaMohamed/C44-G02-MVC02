using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerrService _trainerrService;

        public TrainerController(ITrainerrService trainerrService)
        {
            _trainerrService = trainerrService;
        }
        public IActionResult Index()
        {
            var trainers = _trainerrService.GetAllTrainers();
            return View(trainers);
        }
        public IActionResult TrainerDetails(int Id)
        {
            if (Id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Trainer Not be Zero Or Negative";
                return RedirectToAction(nameof(Index));
            }
            var trainer = _trainerrService.GetTrainerDetails(Id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);

        }

    }
}
