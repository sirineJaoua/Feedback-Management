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
    public class FeedbackController : Controller
    {
        private FeedbackContext db = new FeedbackContext();

        // GET: Feedbacks
        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View(db.Feedbacks.ToList());
            }
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }
        public ActionResult GetFeedback()
        {

            List<Feedback> feedback = GetFeedbackFromModel();


            string jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(feedback, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            var obj = JsonConvert.DeserializeObject<List<Feedback>>(jsonObj);

            var result = Json(obj, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;

        }

        private List<Feedback> GetFeedbackFromModel()
        {

            return db.Feedbacks.ToList();
        }



        public List<FeedbackDetail> Fdetail(int? id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            List<FeedbackDetail> list = feedback.Details.ToList();
            return list;

        }
        public ActionResult UpdatStatue(int? id, int stat)
        { Feedback feedback= db.Feedbacks.Find(id);
            
            Statue statue= db.Status.Find(stat);
            feedback.StatusId = statue.Id;
            db.SaveChanges();
                        return RedirectToAction("Index"); }


        // GET: Feedbacks/Create
        /*    public ActionResult Create()
            {
                return View();
            }*/

        // POST: Feedbacks/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Creationdate")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Creationdate")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }
        public ActionResult DestroyFeedback(int id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
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
