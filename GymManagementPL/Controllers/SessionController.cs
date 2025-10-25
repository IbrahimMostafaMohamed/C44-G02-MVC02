using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public IActionResult Index()
        {
            var Sessions = _sessionService.GetAllSessions();
            return View(Sessions);
        }
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Session Not be Zero Or Negative";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionService.GetSessionById(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "session Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        public IActionResult Create()
        {
            LoadDropdownsForCategories();
            LoadDropdownsForTrainers();

            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateSessionViewModel createdSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdownsForCategories();
                LoadDropdownsForTrainers();
                return View(createdSession);
            }
            bool Result = _sessionService.CreatSession(createdSession);
            if (Result) 
            {
                TempData["SuccessMessage"] = "Session is Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Session failed to Create";
                LoadDropdownsForCategories();
                LoadDropdownsForTrainers();
                return View(createdSession);
            }
        }

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Session Not be Zero Or Negative";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionService.GetSessionToUpdate(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "session Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int id ,UpdateSessionViewModel updateSession )
        {
            if (!ModelState.IsValid)
            {
                LoadDropdownsForTrainers();
                return View(updateSession);
            }
            bool Result = _sessionService.UpdateSession(updateSession , id);
            if (Result)
                TempData["SuccessMessage"] = "Session Updated Successfully";
            else
                TempData["ErrorMessage"] = "Session Failed To Update";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Session Not be Zero Or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Session = _sessionService.GetSessionById(id);
            if (Session == null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = id;
            return View();
        }
        [HttpPost]
        public IActionResult DeleteConfirmed([FromForm] int id)
        {
            var Result = _sessionService.RemoveSession(id);
            if (Result)
                TempData["SuccessMessage"] = "Session Deleted Successfully";
            else
                TempData["ErrorMessage"] = "Session Failed To Delete";
            return RedirectToAction(nameof(Index));

        }

        #region Helper Method

        private void LoadDropdownsForCategories()
        {
            var Categories = _sessionService.GetCategoryForDropdown();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");
          
        }

        private void LoadDropdownsForTrainers()
        {
            var Trainers = _sessionService.GetTrainersForDropdown();
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");
        }

        #endregion
    }
}
