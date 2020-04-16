using System.Web.Mvc;
using DAISIS.Models;

namespace DAISIS.Controllers
{
    public class GamesController : Controller
    {
        private readonly Database<Games> _games = new Database<Games>();
        
        public ActionResult Index()
        {
            ViewBag.Games = _games.Load(new[] {"name"});
            return View();
        }
    }
}