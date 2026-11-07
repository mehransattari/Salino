using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class Monasebat
    {
        [Key]
        public int Id { get; set; }
        //_________________________________________________________________________________
        [Required(ErrorMessage = "لطفا{0}راواردکنید")]
        [MaxLength(50, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شه")]
        [Display(Name = "عنوان مناسبت")]
        public string MonasebatTitle { get; set; }
        //_________________________________________________________________________________
        public virtual ICollection<Product> Products { get; set; }
    }
}