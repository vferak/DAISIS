using System.IO;
using System.Web.Mvc;
using DAISIS.Models;

namespace DAISIS.Controllers
{
    public class GamesController : Controller
    {
        private ActionResult ProcessForm(Games game)
        {
            if (game.image_file != null)
            {
                var mappedPath = HttpContext.Server.MapPath("~/App_Data/Blob/");
                game.image_file.SaveAs(Path.Combine(mappedPath, game.image_file.FileName));
                game.main_image = game.image_file.FileName;
            }
                
            game.Save();
            return RedirectToAction("Show", "Games", new {id = game.LoadOne().gameID});
        }

        public ActionResult Index()
        {
            var gameTime = Request.QueryString["gameTime"];
            var playerCount = Request.QueryString["playerCount"];
            var ageLimit = Request.QueryString["ageLimit"];
            
            ViewBag.Games = new Games().LoadWithRatings(gameTime, playerCount, ageLimit);
            ViewBag.gameTime = gameTime;
            ViewBag.playerCount = playerCount;
            ViewBag.ageLimit = ageLimit;
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
            return ModelState.IsValid ? ProcessForm(game) : Create();
        }

        public ActionResult Update(int? id)
        {
            var game = new Games() {gameID = id}.LoadOne();
            if (game == null) return HttpNotFound();

            ViewBag.Game = game;
            ViewBag.Publishers = new Publishers().Load();
            ViewBag.Designers = new Designers().Load();
            return View();
        }
        
        [HttpPost]
        public ActionResult Update(Games game)
        {
            return ModelState.IsValid ? ProcessForm(game) : Update(game.gameID);
        }
        
        public ActionResult Destroy(int id)
        {
            var game = new Games() {gameID = id}.LoadOne();
            if (game == null) return HttpNotFound();

            game.Delete();
            return RedirectToAction("Index");
        }
    }
}