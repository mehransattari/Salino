using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Salino.Models
{
    public class ImageAdver
    {
        public int Id { get; set; }
        [Display(Name = "  تصویرراست بالا")]
        public string Img1{ get; set; }
        [Display(Name = "  گروه")]
        public int GroupIdTopRight { get; set; }
        //_____________________________________________
        [Display(Name = "  تصویرراست پایین")]
        public string Img2 { get; set; }
        [Display(Name = "  گروه")]
        public int GroupIdBottomRight { get; set; }
        //_____________________________________________
        [Display(Name = "  تصویرچپ بالا")]
        public string Img3 { get; set; }
        [Display(Name = "  گروه")]
        public int GroupIdTopLeft { get; set; }
        //_____________________________________________
        [Display(Name = "  تصویرچپ پایین")]
        public string Img4 { get; set; }
        [Display(Name = "  گروه")]
        public int GroupIdBottomLeft { get; set; }
        //_____________________________________________
 


    }
}