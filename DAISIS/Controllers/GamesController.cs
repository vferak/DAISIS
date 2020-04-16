using System.Web.Mvc;
using DAISIS.Models;

namespace DAISIS.Controllers
{
    public class GamesController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Games = new Games().Load(new[] {"name"});
            return View();
        }
    }
}