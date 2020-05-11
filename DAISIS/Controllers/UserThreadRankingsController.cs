using System.Web.Mvc;
using DAISIS.Models;
using DAISIS.Models.TestModel;

namespace DAISIS.Controllers
{
    public class UserThreadRankingsController : Controller
    {
        public ActionResult Create(int id)
        {
            var ranking = new User_thread_rankings() {threadID = id, userID = SystemValues.DEFAULT_USER_ID, rating = 1};
            if (ranking.LoadOne() == null)
            {
                ranking.Save();
            }

            return RedirectToAction("Show", "Threads", new {id});
        }

        public ActionResult Destroy(int id)
        {
            var ranking = new User_thread_rankings() {threadID = id, userID = SystemValues.DEFAULT_USER_ID, rating = 1};
            if (ranking.LoadOne() != null)
            {
                ranking.Delete();
            }
            
            return RedirectToAction("Show", "Threads", new {id});
        }
    }
}