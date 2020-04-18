using System.Web.Mvc;
using DAISIS.Models.TestModel;

namespace DAISIS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            ViewBag.TestResult = new Test().Run();
            return View();
        }
    }
}