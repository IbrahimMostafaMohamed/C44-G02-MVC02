using GymManagementBLL.Services.Interfaces;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize]
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
