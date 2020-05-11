using System.Web.Mvc;
using DAISIS.Models;

namespace DAISIS.Controllers
{
    public class ThreadsController : Controller
    {
        private ActionResult ProcessForm(Threads thread)
        {
            thread.Save();
            return RedirectToAction("Show", new {id = thread.LoadOne().threadID});
        }

        public ActionResult Index(int gameId)
        {
            ViewBag.GameId = gameId;
            ViewBag.Threads = new Threads(){gameID = gameId}.Load();
            return View();
        }
        
        public ActionResult Show(int id)
        {
            var thread = new Threads() {threadID = id}.LoadOne();
            if (thread == null) return HttpNotFound();
        
            ViewBag.Thread = thread;
            ViewBag.Comments = new Thread_comments() {threadID = id}.Load();
            return View();
        }
        
        public ActionResult Create(int? id)
        {
            ViewBag.Thread = new Threads() {gameID = id, userID = SystemValues.DEFAULT_USER_ID};
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Threads thread)
        {
            return ModelState.IsValid ? ProcessForm(thread) : Create(thread.gameID);
        }
        
        public ActionResult Update(int? id)
        {
            var thread = new Threads() {threadID = id}.LoadOne();
            if (thread == null) return HttpNotFound();
        
            ViewBag.Thread = thread;
            return View();
        }
        
        [HttpPost]
        public ActionResult Update(Threads thread)
        {
            return ModelState.IsValid ? ProcessForm(thread) : Update(thread.threadID);
        }
        
        public ActionResult Destroy(int id)
        {
            var thread = new Threads() {threadID = id}.LoadOne();
            if (thread == null) return HttpNotFound();

            thread.Delete();
            return Redirect(Url.Action("Show", "Games", new {id = thread.gameID}) + "#threads");
        }
    }
}