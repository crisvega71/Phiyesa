//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Phiyesa
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class AutoPart
    {
        private PHIYESA_AutoEntities dbPhiyesa = new PHIYESA_AutoEntities();

        public int Id { get; set; }

        [Display(Name = "Part No.")]
        public string PartNo { get; set; }

        [Display(Name = "Category")]
        public Nullable<int> PartCategory { get; set; }
        public string Year { get; set; }

        [Display(Name = "Make")]
        public Nullable<int> Make { get; set; }

        [Display(Name = "Model")]
        public Nullable<int> Model { get; set; }

        [Display(Name = "Description")]
        public string PartDescription { get; set; }

        [Display(Name = "Brand")]
        public Nullable<int> Brand { get; set; }

        [Display(Name = "Made-In")]
        public Nullable<int> MadeIn { get; set; }

        public Nullable<int> EngineType { get; set; }

        [Display(Name = "SubCategory")]
        public Nullable<int> PartSubCategory { get; set; }

        [Display(Name = "Part Image")]
        public string PartImage { get; set; }

        public string getCategoryDesc
        {
            get
            {
                string category_desc = dbPhiyesa.PartCategories.Find(PartCategory).PartCategoryDesc;
                return category_desc;
            }
        }

        public string getSubCategoryDesc
        {
            get
            {
                string sub_category_desc = dbPhiyesa.PartSubCategories.Find(PartSubCategory).PartSubCategoryDesc;
                return sub_category_desc;
            }
        }

        public string getCarMakeDesc
        {
            get
            {
                string carmake_desc = dbPhiyesa.AutoMakes.Find(Make).CarMake;
                return carmake_desc;
            }
        }

        public string getCarModelDesc
        {
            get
            {
                string carmodel_desc = dbPhiyesa.AutoModels.Find(Model).CarModelDesc;
                return carmodel_desc;
            }
        }

        public string getCarBrandDesc
        {
            get
            {
                string carbrand_desc = dbPhiyesa.AutoPartBrands.Find(Brand).BrandName;
                return carbrand_desc;
            }
        }

        public string getCarMadeInDesc
        {
            get
            {
                string carmadeIn_desc = dbPhiyesa.MadeInCountries.Find(MadeIn).CountryName;
                return carmadeIn_desc;
            }
        }

    }
}
