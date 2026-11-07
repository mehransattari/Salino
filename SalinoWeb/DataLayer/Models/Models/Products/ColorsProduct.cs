using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class ColorsProduct
    {
        [Key]
        public int Id { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "رنگ")]
        public int ColorId { get; set; }
        public virtual Color  color { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "محصول")]
        public int ProductId { get; set; }
     
    }
}