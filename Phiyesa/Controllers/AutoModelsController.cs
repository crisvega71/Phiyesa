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
    public class AutoModelsController : Controller
    {
        private PHIYESA_AutoEntities db = new PHIYESA_AutoEntities();

        // GET: AutoModels
        public ActionResult Index()
        {
            SelectList listOfCarMakes = new SelectList(db.AutoMakes.OrderBy(o => o.CarMake), "Id", "CarMake");
            ViewBag.ListOfCarMake = listOfCarMakes;

            AutoMake firstAutoMake = db.AutoMakes.OrderBy(o => o.CarMake).First();
            int IdCarMake = firstAutoMake.Id;

            IEnumerable<AutoModel> listOfCarModelByMake = db.AutoModels.Where(c => c.CarMake == IdCarMake).ToList();
            ViewBag.SelectedCarMake = db.AutoMakes.Find(IdCarMake).CarMake;

            return View(listOfCarModelByMake);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            SelectList listOfCarMakes = new SelectList(db.AutoMakes, "Id", "CarMake");
            ViewBag.ListOfCarMake = listOfCarMakes;

            var selectedCarMakeID = formCollection["selCarMake"];
            int nCarMakeID = Convert.ToInt32(selectedCarMakeID);

            IEnumerable<AutoModel> listOfCarModelByMake = db.AutoModels.Where(c => c.CarMake == nCarMakeID).ToList();
            ViewBag.SelectedCarMake = db.AutoMakes.Find(nCarMakeID).CarMake;

            return View(listOfCarModelByMake);
        } //--

        // GET: AutoModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoModel autoModel = db.AutoModels.Find(id);
            if (autoModel == null)
            {
                return HttpNotFound();
            }
            return View(autoModel);
        }

        // GET: AutoModels/Create
        public ActionResult Create()
        {
            SelectList listOfCarMake = new SelectList(db.AutoMakes, "Id", "CarMake");
            ViewBag.ListCarMake = listOfCarMake;

            return View();
        }

        // POST: AutoModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CarMake,CarModelDesc")] AutoModel autoModel)
        {
            if (ModelState.IsValid)
            {
                db.AutoModels.Add(autoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autoModel);
        }

        // GET: AutoModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            AutoModel autoModel = db.AutoModels.Find(id);
            if (autoModel == null)
            {   return HttpNotFound();  }

            SelectList listOfCarMake = new SelectList(db.AutoMakes, "Id", "CarMake");
            ViewBag.ListCarMake = listOfCarMake;

            return View(autoModel);
        }

        // POST: AutoModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CarMake,CarModelDesc")] AutoModel autoModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            SelectList listOfCarMake = new SelectList(db.AutoMakes, "Id", "CarMake");
            ViewBag.ListCarMake = listOfCarMake;

            return View(autoModel);
        }

        // GET: AutoModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoModel autoModel = db.AutoModels.Find(id);
            if (autoModel == null)
            {
                return HttpNotFound();
            }
            return View(autoModel);
        }

        // POST: AutoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AutoModel autoModel = db.AutoModels.Find(id);
            db.AutoModels.Remove(autoModel);
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
