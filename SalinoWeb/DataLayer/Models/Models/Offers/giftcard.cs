using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class giftcard
    {
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
    }
}