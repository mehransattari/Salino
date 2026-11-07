using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class OfferNumberShop
    {
        [Key]
        public int Id { get; set; }

        //________________________________________________________________________
        [Display(Name = " چندمین خرید ")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        public int ToNumber { get; set; }
        //________________________________________________________________________
        [Display(Name = " تخفیف ")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        public int PriceDiscount { get; set; }
    }
}