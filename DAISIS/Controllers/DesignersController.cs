using System.Web.Mvc;
using DAISIS.Models;

namespace DAISIS.Controllers
{
    public class DesignersController : Controller
    {
        private ActionResult ProcessForm(Designers designer)
        {
            designer.Save();
            return RedirectToAction("Show", "Designers", new {id = designer.LoadOne().designerID});
        }

        public ActionResult Index()
        {
            ViewBag.Designers = new Designers().Load();
            return View();
        }

        public ActionResult Show(int id)
        {
            var designer = new Designers() {designerID = id}.LoadOne();
            if (designer == null) return HttpNotFound();

            ViewBag.Designer = designer;
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Designers designer)
        {
            return ModelState.IsValid ? ProcessForm(designer) : Create();
        }

        public ActionResult Update(int? id)
        {
            var designer = new Designers() {designerID = id}.LoadOne();
            if (designer == null) return HttpNotFound();

            ViewBag.Designer = designer;
            return View();
        }
        
        [HttpPost]
        public ActionResult Update(Designers designer)
        {
            return ModelState.IsValid ? ProcessForm(designer) : Update(designer.designerID);
        }
        
        public ActionResult Destroy(int id)
        {
            var designer = new Designers() {designerID = id}.LoadOne();
            if (designer == null) return HttpNotFound();

            designer.Delete();
            return RedirectToAction("Index");
        }
    }
}