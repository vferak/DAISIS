using System.Collections.Generic;
using System.IO;
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

        public ActionResult Create()
        {
            ViewBag.Publishers = new Publishers().Load();
            ViewBag.Designers = new Designers().Load();
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Games game)
        {
            if (ModelState.IsValid)
            {
                if (game.image_file != null)
                {
                    var mappedPath = HttpContext.Server.MapPath("~/App_Data/Blob/");
                    game.image_file.SaveAs(Path.Combine(mappedPath, game.image_file.FileName));
                    game.main_image = game.image_file.FileName;
                }
                
                game.Save();
                return RedirectToAction("Index");
            }
            
            ViewBag.Publishers = new Publishers().Load();
            ViewBag.Designers = new Designers().Load();
            return View(game);
        }
    }
}