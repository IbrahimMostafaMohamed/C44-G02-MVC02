using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementBLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize(Roles="SuperAdmin")]
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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTrainer(CreateTrainerViewModel CreatedTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(nameof(Create), CreatedTrainer);
            }
            bool Result = _trainerrService.CreateTrainer(CreatedTrainer);
            if (Result)
                TempData["SuccessMessage"] = "Trainer is Created Successfully";
            else
                TempData["ErrorMessage"] = "Trainer failed Create";
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int Id)
        {
            if (Id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Trainer Not be Zero Or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Trainer = _trainerrService.GetTrainerToUpdate(Id);
            if (Trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Trainer);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, TrainerToUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(model);
            }
            var updateModel = new UpdateTrainerViewModel
            {
                Email = model.Email,
                Phone = model.Phone,
                BuildingNumber = model.BuildingNumber,
                Street = model.Street,
                City = model.City,
                Specialites = model.Specialites
            };

            var result = _trainerrService.UpdateTrainerDetails(updateModel, id);

            if (result)
                TempData["SuccessMessage"] = "Trainer Updated Successfully";
            else
                TempData["ErrorMessage"] = "Trainer Failed To Update";

            return RedirectToAction(nameof(Index));

        }


        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Trainer Not be Zero Or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Trainer = _trainerrService.GetTrainerDetails(id);
            if (Trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerId = id;
            return View();
        }
        [HttpPost]
        public IActionResult DeleteConfirmed([FromForm] int id)
        {
            var Result = _trainerrService.RemoveTrainer(id);
            if (Result)
                TempData["SuccessMessage"] = "Trainer Deleted Successfully";
            else
                TempData["ErrorMessage"] = "Trainer Failed To Delete";
            return RedirectToAction(nameof(Index));

        }


    }
}
