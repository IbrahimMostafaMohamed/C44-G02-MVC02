using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToRoute("Trainer");
        }
        public ActionResult GetMembers()
        {
             return View();
        }
        public ActionResult CreatMembers()
        {
            return View();
        }
    }
}
