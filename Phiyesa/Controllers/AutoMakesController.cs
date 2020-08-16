using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Phiyesa;

namespace Phiyesa.Controllers
{
    [Authorize]
    public class AutoMakesController : Controller
    {
        private PHIYESA_AutoEntities db = new PHIYESA_AutoEntities();

        public string[] carIndexAlphaList = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };


        // GET: AutoMakes
        public ActionResult Index()
        {
            IEnumerable<AutoMake> listCarMake = db.AutoMakes.Where(a => a.CarMake.StartsWith("A")).OrderBy(o => o.CarMake).ToList();

            ViewBag.CarIndexList = carIndexAlphaList;
            ViewBag.CurrentCarIndex = "A";

            return View(listCarMake);
        }


        public ActionResult IndexCarMake(string idx)
        {
            ViewBag.CarIndexList = carIndexAlphaList;
            ViewBag.CurrentCarIndex = idx;

            IEnumerable<AutoMake> listCarMake = db.AutoMakes.Where(a => a.CarMake.StartsWith(idx)).OrderBy(o => o.CarMake).ToList();

            return View(listCarMake);

        } //--

        // GET: AutoMakes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoMake autoMake = db.AutoMakes.Find(id);
            if (autoMake == null)
            {
                return HttpNotFound();
            }
            return View(autoMake);
        }

        // GET: AutoMakes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutoMakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CarMake")] AutoMake autoMake)
        {
            if (ModelState.IsValid)
            {
                db.AutoMakes.Add(autoMake);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autoMake);
        }

        // GET: AutoMakes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoMake autoMake = db.AutoMakes.Find(id);
            if (autoMake == null)
            {
                return HttpNotFound();
            }
            return View(autoMake);
        }

        // POST: AutoMakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CarMake")] AutoMake autoMake)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autoMake).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autoMake);
        }

        // GET: AutoMakes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoMake autoMake = db.AutoMakes.Find(id);
            if (autoMake == null)
            {
                return HttpNotFound();
            }
            return View(autoMake);
        }

        // POST: AutoMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AutoMake autoMake = db.AutoMakes.Find(id);
            db.AutoMakes.Remove(autoMake);
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
