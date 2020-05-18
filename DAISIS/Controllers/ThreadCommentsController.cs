using System.Web.Mvc;
using DAISIS.Models;

namespace DAISIS.Controllers
{
    public class ThreadCommentsController : Controller
    {
        private ActionResult ProcessForm(Thread_comments comment)
        {
            comment.Save();
            return Redirect(Url.Action("Show", "Threads", new {id = comment.threadID}));
        }

        public ActionResult Create(int? id)
        {
            ViewBag.Comment = new Thread_comments() {threadID = id, userID = SystemValues.DEFAULT_USER_ID};
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Thread_comments comment)
        {
            return ModelState.IsValid ? ProcessForm(comment) : Create(comment.commentID);
        }
        
        public ActionResult Update(int? id)
        {
            var comment = new Thread_comments() {commentID = id}.LoadOne();
            if (comment == null) return HttpNotFound();
        
            ViewBag.Comment = comment;
            return View();
        }
        
        [HttpPost]
        public ActionResult Update(Thread_comments comment)
        {
            return ModelState.IsValid ? ProcessForm(comment) : Update(comment.commentID);
        }
        
        public ActionResult Destroy(int id)
        {
            var comment = new Thread_comments() {commentID = id}.LoadOne();
            if (comment == null) return HttpNotFound();

            comment.Delete();
            return Redirect(Url.Action("Show", "Threads", new {id = comment.threadID}));
        }
    }
}