using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class ImageOffer
    {
        [Key]
        public int Id { get; set; }
        //___________________________________________________________________
        [Display(Name = "عنوان عکس")]
        [MaxLength(100, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شود")]
        public string SliderTitle { get; set; }
        //___________________________________________________________________
        [Display(Name = "تصویردرکامپیوتر")]
        public string SliderIMG { get; set; }
        //___________________________________________________________________
        [Display(Name = "تصویر درموبایل")]
        public string SliderIMGMob { get; set; }
        //___________________________________________________________________
    }
}