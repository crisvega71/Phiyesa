using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Phiyesa;
using MiscellClassLib;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace Phiyesa.Controllers
{
    [Authorize]
    public class AutoPartsController : Controller
    {
        private PHIYESA_AutoEntities db = new PHIYESA_AutoEntities();
        public LookUpMethod lookUpMethod = new LookUpMethod();
        public FileUtility fileUtil = new FileUtility();

        // GET: AutoParts
        public ActionResult Index()
        {
            return View(db.AutoParts.ToList());
        }

        // GET: AutoParts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            AutoPart autoPart = db.AutoParts.Find(id);
            if (autoPart == null)
            {   return HttpNotFound();  }

            return View(autoPart);
        }

        // GET: AutoParts/Create
        public ActionResult Create()
        {
            SelectListCommon selCommonObj = new SelectListCommon();

            ViewBag.ListOfPartCategories = selCommonObj.ListOfPartCategories();
            ViewBag.ListOfAutoYears = selCommonObj.listOfAutoYears();
            ViewBag.ListOfCarMakes = selCommonObj.listOfCarMakes(); 
            ViewBag.ListOfAutoBrands = selCommonObj.listOfAutoBrands();
            ViewBag.ListOfCountryMadeIn = selCommonObj.listOfCountryMadeIn();

            return View();
        }

        // POST: AutoParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PartNo,PartCategory,Year,Make,Model,PartDescription,Brand,MadeIn,EngineType,PartSubCategory,PartImage")] AutoPart autoPart)
        {
            if (ModelState.IsValid)
            {
                db.AutoParts.Add(autoPart);
                db.SaveChanges();

                return RedirectToAction("CreatePartII/" + autoPart.Id.ToString());
            }

            return View(autoPart);
        }

        [HttpGet]
        public ActionResult CreatePartII(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            AutoPart autoPart = db.AutoParts.Find(id);
            if (autoPart == null)
            {   return HttpNotFound();  }

            List<PartSubCategory> lstPartSubCategories = db.PartSubCategories.Where(c => c.PartCategory == autoPart.PartCategory).ToList();
            SelectList listOfPartSubCategories = new SelectList(lstPartSubCategories, "Id", "PartSubCategoryDesc");
            ViewBag.ListOfPartSubCategories = listOfPartSubCategories;

            List<AutoModel> lstCarModels = db.AutoModels.Where(m => m.CarMake == autoPart.Make).ToList();
            SelectList listOfCarModels = new SelectList(lstCarModels, "Id", "CarModelDesc");
            ViewBag.ListOfCarModels = listOfCarModels;

            return View(autoPart);
        } //--

        [HttpPost]
        public ActionResult CreatePartII([Bind(Include = "Id,PartNo,Make,Model,PartSubCategory")] AutoPart autoPart)
        {
            int partId = autoPart.Id;

            int? partSubCategory = autoPart.PartSubCategory;

            string US_ConnStr = ConfigurationManager.ConnectionStrings["PHIYESA_ADO"].ConnectionString; ;
            using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
            {
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "Update AutoPart Set PartSubCategory = @sub_category, Model = @car_model  Where Id = @part_id";

                sqlCmd.Parameters.AddWithValue("@sub_category", autoPart.PartSubCategory);
                sqlCmd.Parameters.AddWithValue("@car_model", autoPart.Model);
                sqlCmd.Parameters.AddWithValue("@part_id", autoPart.Id);

                sqlCmd.Connection = sqlCon;
                sqlCon.Open();
                int rowaffected = sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }

            return RedirectToAction("Details/" + autoPart.Id.ToString());

            //return View();
        } //--

        public ActionResult CreateP2Saved()
        {
            string part_id = Request.QueryString["pid"];
            string part_subcategory = Request.QueryString["subcat"];
            string auto_model = Request.QueryString["model"];

            return View();
        }

        // GET: AutoParts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);     }

            AutoPart autoPart = db.AutoParts.Find(id);
            if (autoPart == null)
            {   return HttpNotFound();  }

            SelectListCommon selCommonObj = new SelectListCommon();
            ViewBag.ListOfPartCategories = selCommonObj.ListOfPartCategories();
            ViewBag.ListOfAutoYears = selCommonObj.listOfAutoYears();
            ViewBag.ListOfCarMakes = selCommonObj.listOfCarMakes();
            ViewBag.ListOfAutoBrands = selCommonObj.listOfAutoBrands();
            ViewBag.ListOfCountryMadeIn = selCommonObj.listOfCountryMadeIn();

            return View(autoPart);
        }

        // POST: AutoParts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PartNo,PartCategory,Year,Make,Model,PartDescription,Brand,MadeIn,EngineType,PartSubCategory,PartImage")] AutoPart autoPart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autoPart).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("CreatePartII", new { id = autoPart.Id });
            }

            SelectListCommon selCommonObj = new SelectListCommon();
            ViewBag.ListOfPartCategories = selCommonObj.ListOfPartCategories();
            ViewBag.ListOfAutoYears = selCommonObj.listOfAutoYears();
            ViewBag.ListOfCarMakes = selCommonObj.listOfCarMakes();
            ViewBag.ListOfAutoBrands = selCommonObj.listOfAutoBrands();
            ViewBag.ListOfCountryMadeIn = selCommonObj.listOfCountryMadeIn();

            return View(autoPart);
        } //--

        public ActionResult Image(int? id)
        {
            if (id == null)
            {   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);     }

            AutoPart autoPart = db.AutoParts.Find(id);
            if (autoPart == null)
            {   return HttpNotFound();      }

            return View(autoPart);
        } //--

        [HttpPost]
        public ActionResult Image([Bind(Include = "Id,PartNo")] AutoPart autopart, HttpPostedFileBase fileAutoPart)
        {
            if (fileAutoPart != null)
            {
                string fileName = autopart.Id.ToString() + "-" + autopart.PartNo;
                string file_ext = Path.GetExtension(fileAutoPart.FileName);
                string fileAutoPartImage = fileName + file_ext;

                if (fileUtil.ValidImageExtension(file_ext))
                {
                    string path = Server.MapPath("~/AutoPartImages/" + fileAutoPartImage);
                    fileAutoPart.SaveAs(path);

                    string US_ConnStr = ConfigurationManager.ConnectionStrings["PHIYESA_ADO"].ConnectionString;
                    using (SqlConnection sqlCon = new SqlConnection(US_ConnStr))
                    {
                        SqlCommand sqlCmd = new SqlCommand();
                        sqlCmd.CommandText = "Update AutoPart Set PartImage = @autopart_imagefilename Where Id = @Id";
                        sqlCmd.Parameters.AddWithValue("@autopart_imagefilename", fileAutoPartImage);
                        sqlCmd.Parameters.AddWithValue("@Id", autopart.Id);
                        sqlCmd.Connection = sqlCon;
                        sqlCon.Open();
                        int rowaffected = sqlCmd.ExecuteNonQuery();
                        sqlCon.Close();
                    }
                }
                else
                {
                    ViewBag.FileErrorMessage = "File Upload ERROR: Only JPG files are allowed to upload ... ";
                    return View(autopart);
                }
            } //--

            return RedirectToAction("Details/" + autopart.Id.ToString());
        } //--

        // GET: AutoParts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutoPart autoPart = db.AutoParts.Find(id);
            if (autoPart == null)
            {
                return HttpNotFound();
            }
            return View(autoPart);
        }

        // POST: AutoParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AutoPart autoPart = db.AutoParts.Find(id);
            db.AutoParts.Remove(autoPart);
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
