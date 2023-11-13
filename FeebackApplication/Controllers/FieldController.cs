using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FeebackApplication.DAL;
using FeebackApplication.Models;
using Newtonsoft.Json;

namespace FeebackApplication.Controllers
{
    
    public class FieldController : Controller
    {
       
        private FeedbackContext db = new FeedbackContext();

        // GET: Field
      
        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View(db.Fields.ToList());
            }
        }
        public ActionResult Succes()
        {
            return View();
        }

        // GET: Field/Details/5
        [HttpGet]
        
            public ActionResult WindowInfo(int id)
            {
                Field field = db.Fields.FirstOrDefault(f => f.Id == id);
                if (field == null)
                {
                    return HttpNotFound();
                }
                return View(field);
            }
        
  
        
        public ActionResult GetField()
        {
           
            List<Field> field= GetFieldFromModel();
            
            
                string jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(field, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                var obj = JsonConvert.DeserializeObject<List<Field>>(jsonObj);

            var result = Json(obj, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;

        }

        private List<Field> GetFieldFromModel()
        {
            
            return db.Fields.ToList();
        }

        public int CreateContact(FormCollection form)
        {
            List<String> FormItem = new List<String>();
            List<int> FormID = new List<int>();
         

            foreach (string fieldName in form)
            {
                if (fieldName.StartsWith("Fields["))
                {

                    int fieldId = int.Parse(fieldName.Substring(fieldName.IndexOf("[") + 1, fieldName.IndexOf("]") - fieldName.IndexOf("[") - 1));
                    FormID.Add(fieldId);
                    string value = form[fieldName];
                    FormItem.Add(value);
                }
            }
            foreach (int id in FormID)
            { Field field = db.Fields.FirstOrDefault(x => x.Id == id);
                
                if(field != null)
                {
                    switch (field.Name.ToLower())
                    {
                        case "first name":
                            int index = FormID.IndexOf(id);
                            string value=FormItem[0];
                            FormItem[0]=FormItem[index];
                            FormItem[index]=value; 

                            break;
                        case "last name":
                            int inde = FormID.IndexOf(id);
                            string valu = FormItem[1];
                            FormItem[1] = FormItem[inde];
                            FormItem[inde] = valu;

                            break;
                        case "email":
                            int ind = FormID.IndexOf(id);
                            string val = FormItem[2];
                            FormItem[2] = FormItem[ind];
                            FormItem[ind] = val;

                            break;
                        case "phone":
                            int indd = FormID.IndexOf(id);
                            string vall = FormItem[3];
                            FormItem[3] = FormItem[indd];
                            FormItem[indd] = vall;

                            break;
                            default:break;


                    }
                    
                }
            }

                string email = FormItem[2];
                int phone = Convert.ToInt32(FormItem[3]);

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
        public ActionResult Formular(FormCollection form)
        {
            var ContactId = CreateContact(form);
            
            Feedback feedback = new Feedback
            {
                ContactId = ContactId,
                Creationdate = DateTime.Now,
                StatusId = 6
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
                    db.SaveChanges();

                   
                    Console.WriteLine("Field Id: " + fieldId + ", Value: " + value);
                }
            }

            return RedirectToAction("Succes");
        }
        public string Range()
        {
            var range = db.Fields.FirstOrDefault(x => x.Name == "Score" && x.ScoreRange != null)?.ScoreRange ;
            var value = range != null ? range.ToString() : "0";
            
            Debug.WriteLine("Range value: " + value);

            return (value);
        }
        public ActionResult Formular()
        {
            var fields = db.Fields.ToList();

            if (fields.Count == 0)
            {
               
                return View("NoFieldsAvailable");
            }

            return View(fields);
        }

    


        // GET: Field/Create
        public ActionResult Create(int? ID = null)
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                Field model = new Field();
                if (ID != null)
                {
                    model = db.Fields.FirstOrDefault(x => x.Id == ID);
                }
                return View(model);
            }
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Field field)
        {
            if (ModelState.IsValid)
            {
                if (field.Id != 0)
                {
                    Field existingField = db.Fields.Find(field.Id);
                    if (existingField != null)
                    {
                        // Update the existing field with the edited values
                        existingField.Name = field.Name;
                        existingField.Type = field.Type;
                        existingField.IsRequired = field.IsRequired;
                        existingField.ScoreRange = field.ScoreRange;

                        // Save the changes to the database
                        db.SaveChanges();

                        return RedirectToAction("Index"); // Redirect to the appropriate action after successful edit
                    }
                }
                else
                {
                    // Create a new field
                    db.Fields.Add(field);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                    // Redirect to the appropriate action after successful create
                }
            }

            return View(field);
        }

        // GET: Field/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Field field = db.Fields.Find(id);
                if (field == null)
                {
                    return HttpNotFound();
                }
                return View(field);
            }
        }

        // POST: Field/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Type,IsRequired,ScoreRange")] Field field)
        {
            if (ModelState.IsValid)
            {
                db.Entry(field).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(field);
        }
    
        public ActionResult DestroyField(int id)
        {
            Field field = db.Fields.Find(id);
            db.Fields.Remove(field);
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
