using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class FarbicTypeProducts
    {
        [Key]
        public int Id { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "پارچه")]
        public int FarbicTypeId { get; set; }
        public virtual FarbicType FarbicTypes { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "محصول")]
        public int ProductId { get; set; }
        public virtual Product Products { get; set; }
    }
}