using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phiyesa.Controllers
{
    public class HomeController : Controller
    {
        private PHIYESA_AutoEntities db = new PHIYESA_AutoEntities();
        public string[] partStylesClass = { "part_div_01", "part_div_02", "part_div_03", "part_div_04" };
        public string[] partCategoryNames = new string[10];
        public string[,] partSubCategoryNames = new string[10,10];

        public ActionResult Index()
        {
            List<PartCategory> partCategories = db.PartCategories.ToList();

            int index = 0;
            foreach (PartCategory category in partCategories)
            {
                partCategoryNames[index] = category.PartCategoryDesc.ToString();

                int pidx = 0;
                List<PartSubCategory> partSubCategories = db.PartSubCategories.Where(p => p.PartCategory == category.Id).ToList();

                foreach (PartSubCategory sub_category in partSubCategories)
                {
                    partSubCategoryNames[index, pidx] = sub_category.PartSubCategoryDesc.ToString();

                    pidx++;
                }

                index++;
            }

            ViewBag.PartStyleClass = partStylesClass;
            ViewBag.PartCategories = partCategoryNames;
            ViewBag.PartSubCategories = partSubCategoryNames;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}