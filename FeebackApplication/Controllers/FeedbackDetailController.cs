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

namespace FeebackApplication.Controllers
{
    public class FeedbackDetailController : Controller
    {
        private FeedbackContext db = new FeedbackContext();

        // GET: FeedbackDetail
        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                var feedbackDetails = db.FeedbackDetails.Include(f => f.Feedback).Include(f => f.Field);
                return View(feedbackDetails.ToList());
            }
        }
         
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<String> values = new List<String>();
            List <FeedbackDetail> feedbackDetail = db.FeedbackDetails.Where(fd => fd.FeedbackId == id).ToList();
            if (feedbackDetail == null)
            {
                return HttpNotFound();
            }
             foreach(var detail in feedbackDetail)
                {   values.Add(detail.Field.Name);
                    values.Add(detail.Value);
                }
            if (values == null)
            {
                return HttpNotFound();
            }
            else { return View(values); }

            
        }

        // GET: FeedbackDetail/Create
     /*   public ActionResult Create()
        {
            ViewBag.FeedbackId = new SelectList(db.Feedbacks, "Id", "Id");
            ViewBag.FieldId = new SelectList(db.Fields, "Id", "Name");
            return View();
        }*/

        // POST: FeedbackDetail/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FeedbackId,Value,FieldId")] FeedbackDetail feedbackDetail)
        {
            if (ModelState.IsValid)
            {
                db.FeedbackDetails.Add(feedbackDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FeedbackId = new SelectList(db.Feedbacks, "Id", "Id", feedbackDetail.FeedbackId);
            ViewBag.FieldId = new SelectList(db.Fields, "Id", "Name", feedbackDetail.FieldId);
            return View(feedbackDetail);
        }

        // GET: FeedbackDetail/Edit/5
        /*
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackDetail feedbackDetail = db.FeedbackDetails.Find(id);
            if (feedbackDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.FeedbackId = new SelectList(db.Feedbacks, "Id", "Id", feedbackDetail.FeedbackId);
            ViewBag.FieldId = new SelectList(db.Fields, "Id", "Name", feedbackDetail.FieldId);
            return View(feedbackDetail);
        }*/

        // POST: FeedbackDetail/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FeedbackId,Value,FieldId")] FeedbackDetail feedbackDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedbackDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FeedbackId = new SelectList(db.Feedbacks, "Id", "Id", feedbackDetail.FeedbackId);
            ViewBag.FieldId = new SelectList(db.Fields, "Id", "Name", feedbackDetail.FieldId);
            return View(feedbackDetail);
        }

        // GET: FeedbackDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackDetail feedbackDetail = db.FeedbackDetails.Find(id);
            if (feedbackDetail == null)
            {
                return HttpNotFound();
            }
            return View(feedbackDetail);
        }

        // POST: FeedbackDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FeedbackDetail feedbackDetail = db.FeedbackDetails.Find(id);
            db.FeedbackDetails.Remove(feedbackDetail);
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
