using GetttingStartedWithEF6.DAL;
using GetttingStartedWithEF6.Models;
using PagedList;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;

namespace GetttingStartedWithEF6.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Student
        public ActionResult Index(string sortorder, string currentFilter, string searchName, int? page)
        {
            if (string.IsNullOrEmpty(sortorder))
            {
                sortorder = "lastname_asc";
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                page = 1;
            }
            else
            {
                searchName = currentFilter;
            }
            ViewBag.CurrentFilter = searchName;

            var query = db.Students.AsQueryable();
            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(searchName.ToLower())
                    || x.LastName.ToLower().Contains(searchName.ToLower()));
            }
            switch (sortorder)
            {
                case "firstname_asc":
                    query = query.OrderBy(x => x.FirstName);
                    break;
                case "firstname_desc":
                    query = query.OrderByDescending(x => x.FirstName);
                    break;
                case "lastname_desc":
                    query = query.OrderByDescending(x => x.LastName);
                    break;
                case "date_asc":
                    query = query.OrderBy(x => x.EnrollmentDate);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(x => x.EnrollmentDate);
                    break;
                default:
                    query = query.OrderBy(x => x.LastName);
                    break;
            }
            ViewBag.FirstNameSortParam = sortorder == "firstname_asc" ? "firstname_desc" : "firstname_asc";
            ViewBag.LastNameSortParam = sortorder == "lastname_asc" ? "lastname_desc" : "lastname_asc";
            ViewBag.EnrollDateSortParam = sortorder == "date_asc" ? "date_desc" : "date_asc";
            ViewBag.CurrentSort = sortorder;

            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException ex)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id, bool? savechangeserror = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (savechangeserror.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Student student = new Student { Id = id };
                db.Entry(student).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (RetryLimitExceededException ex)
            {
                return RedirectToAction("Delete", new { id, savechangeserror = true });
            }
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
