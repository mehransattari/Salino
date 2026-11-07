using Salino.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class MonasebatProducts
    {
        [Key]
        public int Id { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "مناسبت")]
        public int MonasebatId { get; set; }
        public virtual Monasebat Monasebats { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "محصول")]
        public int ProductId { get; set; }
        public virtual Product Products { get; set; }
    }
}