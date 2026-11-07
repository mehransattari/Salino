using Salino.ToShamsi;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class OfferPrice
    {
        public OfferPrice()
        {
          this.Createdate = DateTime.Now;
        }
    
        [Key]
        public int Id { get; set; }
        //________________________________________________________________________
        [Display(Name = "مبلغ تخفیف")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]    
        public int Price { get; set; }
        //________________________________________________________________________
        [Display(Name = "چندمی به بعد")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        public int AsNumber { get; set; }
        //______________________________________________________________________      
        [Display(Name = "تاریخ ایجاد")]
        public DateTime Createdate { get; set; }
    }
}