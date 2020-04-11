using System.Web.Mvc;
using DAISIS.Models;

namespace DAISIS.Controllers
{
    public class PublishersController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Publishers = new Database<Publishers>().Load();
            return View();
        }
    }
}