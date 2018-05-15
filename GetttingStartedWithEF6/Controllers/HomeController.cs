using GetttingStartedWithEF6.DAL;
using GetttingStartedWithEF6.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetttingStartedWithEF6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            using (var context = new SchoolContext())
            {
                //List<EnrollmentDateGroup> groups = (from student in context.Students
                //                                    group student by student.EnrollmentDate into dategroup
                //                                    select new EnrollmentDateGroup
                //                                    {
                //                                        EnrollmentDate = dategroup.Key,
                //                                        StudentCount = dategroup.Count()
                //                                    }).ToList();
                string query = "select count(1) as StudentCount, EnrollmentDate from Person " +
                    "where Discriminator = 'Student' " +
                    "group by EnrollmentDate";
                List<EnrollmentDateGroup> groups = context.Database.SqlQuery<EnrollmentDateGroup>(query).ToList();
                return View(groups);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}