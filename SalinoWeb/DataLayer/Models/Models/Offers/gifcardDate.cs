using Salino.ToShamsi;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class gifcardDate
    {
        public gifcardDate()
        {
     
          
            this.CreateDate=DateTime.Now.Date;
 
        }        
        [Key]
        public int Id { get; set; }
        //____________________________________________________________________
        [Display(Name = "کدتخفیف")]
        [Required(ErrorMessage = "لطفا{0}راواردکنید")]
        [MaxLength(100, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شه")]
        public string Code { get; set; }
        //____________________________________________________________________
        [Display(Name = "مبلغ تخفیف")]
        [Required(ErrorMessage = "لطفا{0}راواردکنید")]
        public int Amount { get; set; }
        //____________________________________________________________________
        [Display(Name = "تعداد استفاده")]
        [Required(ErrorMessage = "لطفا{0}راواردکنید")]
        public int NumberUse { get; set; }
        //____________________________________________________________________
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "تاریخ ایجاد شمسی")]
        public string CreateDateShamsi { get; set; }
        //____________________________________________________________________
        [Display(Name = "تاریخ انقضا")]
        public DateTime ExpirationDate { get; set; }
        [Display(Name = "تاریخ انقضا شمسی")]
        public string ExpirationDateShamsi { get; set; }
        //____________________________________________________________________
    }
}