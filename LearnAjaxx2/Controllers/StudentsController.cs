using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LearnAjaxx2;

namespace LearnAjaxx2.Controllers
{
    public class StudentsController : Controller
    {
        private StudentEntities db = new StudentEntities();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }
        [HttpPost]
        public ActionResult createStudent([Bind(Include = "id,studentName,studentAddress")] Student std)
        {
            db.Students.Add(std);
            db.SaveChanges();
            string message = "SUCCESS";
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }
        public JsonResult getStudent(string id)
        {
            List<Student> students = new List<Student>();
            students = db.Students.ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
        }





    }


}
