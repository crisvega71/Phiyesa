using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiscellClassLib;

namespace Phiyesa.Controllers
{
    public class SelectListCommon
    {
        private PHIYESA_AutoEntities db = new PHIYESA_AutoEntities();
        public LookUpMethod lookUpMethod = new LookUpMethod();

        public SelectList ListOfPartCategories()
        {
            SelectList listOfPartCategories = new SelectList(db.PartCategories, "Id", "PartCategoryDesc");
            return listOfPartCategories;
        }

        public SelectList listOfAutoYears()
        {
            SelectList listOfAutoYears = new SelectList(lookUpMethod.autoYears);
            return listOfAutoYears;
        }

        public SelectList listOfCarMakes()
        {
            SelectList listOfCarMakes = new SelectList(db.AutoMakes, "Id", "CarMake");
            return listOfCarMakes;
        }

        public SelectList listOfAutoBrands()
        {
            SelectList listOfAutoBrands = new SelectList(db.AutoPartBrands, "Id", "BrandName");
            return listOfAutoBrands;
        }

        public SelectList listOfCountryMadeIn()
        {
            SelectList listOfCountryMadeIn = new SelectList(db.MadeInCountries, "Id", "CountryName");
            return listOfCountryMadeIn;
        }

    } //-- end of class
}