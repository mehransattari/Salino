using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salino.Models
{
    public class FarbicType
    {
        [Key]
        public int Id { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "نوع پارچه")]
        [MaxLength(100, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شود")]
        public string tiltle { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "قیمت فروش ")]
        public Int64 PriceMain { get; set; }
        //_________________________________________________________________________________
        [Display(Name = " وضعیت نمایش")]
        public bool  IsShow { get; set; }
        //_________________________________________________________________________________
        public virtual ICollection<Product> Products { get; set; }
    }
}
