using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FeebackApplication.DAL;
using FeebackApplication.Models;
using Newtonsoft.Json;

namespace FeebackApplication.Controllers
{
    public class StatueController : Controller
    {
        private FeedbackContext db = new FeedbackContext();

        // GET: Statue
        public ActionResult Index()
        {
            return View(db.Status.ToList());
        }
        public ActionResult Names()
        {
            List <Statue> state = db.Status.ToList();
           

            string jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(state, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings { });
            var obj = JsonConvert.DeserializeObject<List<Statue>>(jsonObj);
            return Json(obj, JsonRequestBehavior.AllowGet); ;
        }

        // GET: Statue/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statue statue = db.Status.Find(id);
            if (statue == null)
            {
                return HttpNotFound();
            }
            return View(statue);
        }

        // GET: Statue/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Statue/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Statue statue)
        {
            if (ModelState.IsValid)
            {
                db.Status.Add(statue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statue);
        }

        // GET: Statue/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statue statue = db.Status.Find(id);
            if (statue == null)
            {
                return HttpNotFound();
            }
            return View(statue);
        }

        // POST: Statue/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Statue statue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statue);
        }

        // GET: Statue/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statue statue = db.Status.Find(id);
            if (statue == null)
            {
                return HttpNotFound();
            }
            return View(statue);
        }

        // POST: Statue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Statue statue = db.Status.Find(id);
            db.Status.Remove(statue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
