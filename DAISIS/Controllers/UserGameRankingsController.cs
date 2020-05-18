using System.Web.Mvc;
using DAISIS.Models;

namespace DAISIS.Controllers
{
    
    /// <summary>
    /// Bude ještě třeba vyřešit systém typů uživatelů k fungování dle očekávání
    /// </summary>
    public class UserGameRankingsController : Controller
    {
        private ActionResult ProcessForm(User_game_rankings ranking)
        {
            if (ranking.text == null)
            {
                ranking.text = "";
            }

            ranking.Save();
            return Redirect(Url.Action("Show", "Games", new {id = ranking.gameID}) + "#ratings");
        }

        public ActionResult Index(int gameId)
        {
            ViewBag.GameId = gameId;
            ViewBag.Rankings = new User_game_rankings(){gameID = gameId}.Load();
            return View();
        }

        public ActionResult Create(int? id)
        {
            ViewBag.Ranking = new User_game_rankings() {gameID = id, userID = SystemValues.DEFAULT_USER_ID};
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(User_game_rankings ranking)
        {
            return ModelState.IsValid ? ProcessForm(ranking) : Create(ranking.gameID);
        }
        
        public ActionResult Update(int? id, int? subId)
        {
            var ranking = new User_game_rankings() {gameID = id, userID = subId}.LoadOne();
            if (ranking == null) return HttpNotFound();
        
            ViewBag.Ranking = ranking;
            return View();
        }
        
        [HttpPost]
        public ActionResult Update(User_game_rankings ranking)
        {
            return ModelState.IsValid ? ProcessForm(ranking) : Update(ranking.gameID, ranking.userID);
        }
        
        public ActionResult Destroy(int id, int subId)
        {
            var ranking = new User_game_rankings() {gameID = id, userID = subId}.LoadOne();
            if (ranking == null) return HttpNotFound();

            ranking.Delete();
            return Redirect(Url.Action("Show", "Games", new {id = ranking.gameID}) + "#ratings");
        }
    }
}