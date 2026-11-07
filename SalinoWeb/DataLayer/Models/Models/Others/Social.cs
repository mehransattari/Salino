using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class Social
    {
        [Key]

        public int Id { get; set; }
        //________________________________________________________________________
        [Display(Name = "نام شبکه")]
        [Required(ErrorMessage = "لطفا{0}راواردکنید")]
        [MaxLength(100, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شه")]
        public string SocialName { get; set; }
        //________________________________________________________________________
        [Display(Name = "آیکون")]
     
        public string SocialIcon { get; set; }
        //________________________________________________________________________
        [Display(Name = "لینک شبکه")]     
        [MaxLength(200, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شه")]
        public string SocialLink { get; set; }
        //________________________________________________________________________
        [Display(Name = "عدم نمایش")]
        public bool NotShow { get; set; }
        //________________________________________________________________________
        [Display(Name = "ترتیب نمایش")]
        public int SocialOrder { get; set; }
    }
}