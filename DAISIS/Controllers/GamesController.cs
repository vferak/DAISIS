using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web.Mvc;
using DAISIS.Models;

namespace DAISIS.Controllers
{
    public class GamesController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Games = new Games().Load();
            ViewBag.Publishers = new Publishers().Load();
            return View();
        }

        public ActionResult Show(int id)
        {
            var game = new Games() {gameID = id}.LoadOne();
            
            if (game == null) return HttpNotFound();

            ViewBag.Game = game;
            return View();
        }
    }
}