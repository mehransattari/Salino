using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        //_________________________________________________________________________________
      
        [Display(Name = "درباره ما")]
        [Required(ErrorMessage = "لطفا{0}راواردکنید")]
        [DataType(DataType.MultilineText)]
              
        public string DecAboutMe { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "تماس با ما")]
          [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "لطفا{0}راواردکنید")]
        
        public string DecContactUs { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "سوالات متداول")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "لطفا{0}راواردکنید")]
        
        public string Questions { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "آدرس")]
        [DataType(DataType.MultilineText)]      
        [MaxLength(300)]

        public string Address { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "تلفن")]

        [MaxLength(50)]
        public string Tell { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "موبایل")]

        [MaxLength(50)]
        public string Mobile { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "ایمیل")]

        [MaxLength(50)]
        public string Email { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "عکس برند فوتر")]
        public string imageSrc { get; set; }
        [Display(Name = "عکس برند اصلی")]
        public string imageSrcMain { get; set; }
    }
}