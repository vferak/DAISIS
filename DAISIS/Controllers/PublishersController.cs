using System.Collections.Generic;
using System.Web.Mvc;
using DAISIS.Models;

namespace DAISIS.Controllers
{
    public class PublishersController : Controller
    {
        public ActionResult Index()
        {
            var publishers = Model<Publishers>.Load();
            return View();
        }
    }
}