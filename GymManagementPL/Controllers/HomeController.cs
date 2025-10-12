using GymManagementBLL.Services.Interfaces;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalyticsServices _analyticsServices;

        public HomeController(IAnalyticsServices analyticsServices)
        {
            _analyticsServices = analyticsServices;
        }
        public ActionResult Index()
        {
            var Data = _analyticsServices.GetAnalyticsData();
            return View(Data);
        }


    }
}
