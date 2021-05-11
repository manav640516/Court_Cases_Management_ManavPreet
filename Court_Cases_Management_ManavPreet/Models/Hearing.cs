using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Court_Cases_Management_ManavPreet.Models
{
    public class Hearing
    {
        public int ID { get; set; }
        public int CaseID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        [Display(Name = "Current Date")]
        public DateTime CurrentDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        [Display(Name = "Next Date")]
        public DateTime NextDate { get; set; }

        public Case Case { get; set; }
    }
}