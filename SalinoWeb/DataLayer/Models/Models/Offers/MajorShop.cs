using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Salino.Models
{
    public class MajorShop
    {
        public MajorShop()
        {
            this.Createdate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        //________________________________________________________________________
        [Display(Name = "حداقل تعداد انتخاب طرح")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        public int MinSelectedProduct { get; set; }
        //________________________________________________________________________
        [Display(Name = "تعداد هر طرح")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        public int Count { get; set; }
        //________________________________________________________________________
        [Display(Name = "نوع پارچه  ")]
        public int FarbicId { get; set; }
        [Display(Name = "قیمت عمده ")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        public int PriceMajor { get; set; }
        //______________________________________________________________________      
        [Display(Name = "تاریخ ایجاد")]
        public DateTime Createdate { get; set; }
        [ForeignKey("FarbicId")]
        public virtual FarbicType FarbicType { get; set; }
    }
}