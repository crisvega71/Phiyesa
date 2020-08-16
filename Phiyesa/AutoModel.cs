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

    public partial class AutoModel
    {
        private PHIYESA_AutoEntities dbPhiyesa = new PHIYESA_AutoEntities();

        public int Id { get; set; }

        [Display(Name = "Car Make")]
        [Required(ErrorMessage = "*")]
        public Nullable<int> CarMake { get; set; }

        [Display(Name = "Car Model")]
        [Required(ErrorMessage = "*")]
        public string CarModelDesc { get; set; }

        public string getCarMakeDesc
        {
            get
            {
                string car_make_desc = dbPhiyesa.AutoMakes.Find(CarMake).CarMake;
                return car_make_desc;
            }
        }
    }
}
