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
    public class PartSubCategoriesController : Controller
    {
        private PHIYESA_AutoEntities db = new PHIYESA_AutoEntities();

        // GET: PartSubCategories
        public ActionResult Index()
        {
            return View(db.PartSubCategories.ToList());
        }

        // GET: PartSubCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartSubCategory partSubCategory = db.PartSubCategories.Find(id);
            if (partSubCategory == null)
            {
                return HttpNotFound();
            }
            return View(partSubCategory);
        }

        // GET: PartSubCategories/Create
        public ActionResult Create()
        {
            SelectList listSubCategories = new SelectList(db.PartCategories, "Id", "PartCategoryDesc");
            ViewBag.ListOfPartCategories = listSubCategories;

            return View();
        }

        // POST: PartSubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PartCategory,PartSubCategoryDesc")] PartSubCategory partSubCategory)
        {
            if (ModelState.IsValid)
            {
                db.PartSubCategories.Add(partSubCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partSubCategory);
        }

        // GET: PartSubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartSubCategory partSubCategory = db.PartSubCategories.Find(id);
            if (partSubCategory == null)
            {
                return HttpNotFound();
            }
            return View(partSubCategory);
        }

        // POST: PartSubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PartCategory,PartSubCategoryDesc")] PartSubCategory partSubCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partSubCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partSubCategory);
        }

        // GET: PartSubCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartSubCategory partSubCategory = db.PartSubCategories.Find(id);
            if (partSubCategory == null)
            {
                return HttpNotFound();
            }
            return View(partSubCategory);
        }

        // POST: PartSubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PartSubCategory partSubCategory = db.PartSubCategories.Find(id);
            db.PartSubCategories.Remove(partSubCategory);
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
