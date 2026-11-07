using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; }

        //_________________________________________________________________________________
        [Display(Name = "رنگ محصول")]
        [MaxLength(100, ErrorMessage = "نباید بیش تر از {1} کاراکتر باشد")]
        public string color { get; set; }
        //_________________________________________________________________________________
        public virtual ICollection<ColorsProduct> ColorsProduct { get; set; }
    }
}