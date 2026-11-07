using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class SendPrice
    {
        [Key]
        public int Id { get; set; }
        //________________________________________________________________________
  
        //________________________________________________________________________
        [Display(Name = " چندمین انتخاب  برای ارسال رایگان")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        public int AsNumber { get; set; }
    }
}