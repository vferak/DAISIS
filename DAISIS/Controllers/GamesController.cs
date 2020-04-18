using System.Collections.Generic;
using System.Web.Mvc;
using DAISIS.Models;

namespace DAISIS.Controllers
{
    public class GamesController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Games = new Games().Load();
            var game = new Games
            {
                name = "Blood Chill", 
                designerID = 1, 
                publisherID = 1,
                min_game_time = 20,
                min_player_count = 2,
                max_game_time = 40,
                max_player_count = 4,
                age_limit = 10,
                description = "popisek"
            };
            game.Save();
            return View();
        }
    }
}