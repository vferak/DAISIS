using System.Web.Mvc;
using DAISIS.Models;

namespace DAISIS.Controllers
{
    public class PublishersController : Controller
    {
        private ActionResult ProcessForm(Publishers publisher)
        {
            publisher.Save();
            return RedirectToAction("Show", "Publishers", new {id = publisher.LoadOne().publisherID});
        }

        public ActionResult Index()
        {
            ViewBag.Publishers = new Publishers().Load();
            return View();
        }

        public ActionResult Show(int id)
        {
            var publisher = new Publishers() {publisherID = id}.LoadOne();
            if (publisher == null) return HttpNotFound();

            ViewBag.Publisher = publisher;
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Publishers publisher)
        {
            return ModelState.IsValid ? ProcessForm(publisher) : Create();
        }

        public ActionResult Update(int? id)
        {
            var publisher = new Publishers() {publisherID = id}.LoadOne();
            if (publisher == null) return HttpNotFound();

            ViewBag.Publisher = publisher;
            return View();
        }
        
        [HttpPost]
        public ActionResult Update(Publishers publisher)
        {
            return ModelState.IsValid ? ProcessForm(publisher) : Update(publisher.publisherID);
        }
        
        public ActionResult Destroy(int id)
        {
            var publisher = new Publishers() {publisherID = id}.LoadOne();
            if (publisher == null) return HttpNotFound();

            publisher.Delete();
            return RedirectToAction("Index");
        }
    }
}