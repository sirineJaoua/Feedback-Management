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
    public class NewModelController : Controller
    {
        private FeedbackContext db = new FeedbackContext();

        // GET: NewModels
        public ActionResult Index()

        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Index", "User");

            }
            else
            {
                return View(db.NewModels.ToList());
            }
        }

        public ActionResult Form()
        {
            List<Field> fields = db.Fields.ToList();
            List<Product> products = db.Products.ToList();

            // Create an instance of NewModel and populate its properties
            NewModel newModel = new NewModel
            {
                Id = 1, // Set the Id as needed
                Fields = fields,
                ProductList = products
            };

            return View(newModel);
        }
        public ActionResult Succes()
        {
            return View();
        }

        /* public ActionResult Form(NewModel newModel)
         {

                 if (ModelState.IsValid)
                 {
                     db.NewModels.Add(newModel);
                     db.SaveChanges();

                     return RedirectToAction("Form","NewModel");
             }

             newModel.Fields = db.Fields.ToList();
             newModel.Products = db.Products.ToList();

             return View();

         }*/


        public int CreateContact(FormCollection form)
        {
            List<String> FormItem = new List<String>();
            foreach (string fieldName in form)
            {
                if (fieldName.StartsWith("Fields["))
                {

                    int fieldId = int.Parse(fieldName.Substring(fieldName.IndexOf("[") + 1, fieldName.IndexOf("]") - fieldName.IndexOf("[") - 1));
                    string value = form[fieldName];
                    FormItem.Add(value);
                }
            }
            string email = FormItem[2];

            int phone = (int)Convert.ToInt32(FormItem[3]);

            Contact contact = new Contact();

            contact = db.Contacts.FirstOrDefault(x => x.Email.ToLower() == email.ToLower() || x.phone == phone);
            if (contact == null)
            {

                // Create a new Contact object
                contact = new Contact

                {
                    FirstName = FormItem[0],
                    LastName = FormItem[1],
                    Email = FormItem[2],
                    phone = Convert.ToInt32(FormItem[3])
                };
                db.Contacts.Add(contact);
                db.SaveChanges();
            }
            else
            {
                contact.FirstName = FormItem[0];
                contact.LastName = FormItem[1];
                contact.Email = FormItem[2];
                contact.phone = Convert.ToInt32(FormItem[3]);
            }
            return contact.Id;
        }

        [HttpPost]
        public ActionResult Form(FormCollection form)
        {
            var ContactId = CreateContact(form);

            Feedback feedback = new Feedback
            {
                ContactId = ContactId,
                Creationdate = DateTime.Now,
                StatusId = 1

            };

            db.Feedbacks.Add(feedback);
            db.SaveChanges();

            foreach (string fieldName in form)
            {
                if (fieldName.StartsWith("Fields["))
                {
                    int fieldId = int.Parse(fieldName.Substring(fieldName.IndexOf("[") + 1, fieldName.IndexOf("]") - fieldName.IndexOf("[") - 1));
                    string value = form[fieldName];

                    var detail = new FeedbackDetail
                    {
                        FeedbackId = feedback.Id,
                        FieldId = fieldId,

                        Value = value
                    };

                    db.FeedbackDetails.Add(detail);

                }

            }

            foreach (string key in form)
            {

                //product_6_rating
                if (key.StartsWith("product_") && key.EndsWith("_rating"))
                {

                    int firstUnderscoreIndex = key.IndexOf('_');

                    int secondUnderscoreIndex = key.IndexOf('_', firstUnderscoreIndex + 1);

                    if (firstUnderscoreIndex >= 0 && secondUnderscoreIndex > firstUnderscoreIndex)
                    {
                        int prodId = int.Parse(key.Substring(firstUnderscoreIndex + 1, secondUnderscoreIndex - firstUnderscoreIndex - 1));
                        //int val = int.TryParse(form[key], out int rating);

                        // if (int.TryParse(form[key], out int val))

                        var rev = new ProductReview
                        {
                            FeedbackId = feedback.Id,
                            ProductId = prodId,
                            Value = (form[key].ToString())

                        };
                        db.ProductReviews.Add(rev);
                        db.SaveChanges();



                    }
                }

            }

            db.SaveChanges();

            return RedirectToAction("Succes");

        }





        // GET: NewModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewModel newModel = db.NewModels.Find(id);
            if (newModel == null)
            {
                return HttpNotFound();
            }
            return View(newModel);
        }

        // GET: NewModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewModel/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id")] NewModel newModel)
        {
            if (ModelState.IsValid)
            {
                db.NewModels.Add(newModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newModel);
        }

        // GET: NewModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewModel newModel = db.NewModels.Find(id);
            if (newModel == null)
            {
                return HttpNotFound();
            }
            return View(newModel);
        }

        // POST: NewModel/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] NewModel newModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newModel);
        }

        // GET: NewModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewModel newModel = db.NewModels.Find(id);
            if (newModel == null)
            {
                return HttpNotFound();
            }
            return View(newModel);
        }

        // POST: NewModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewModel newModel = db.NewModels.Find(id);
            db.NewModels.Remove(newModel);
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
