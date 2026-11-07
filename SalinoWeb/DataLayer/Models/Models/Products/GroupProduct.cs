using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

namespace Salino.Models
{
    public class GroupProduct
    {

        [Key]
        public int Id { get; set; }
        //_________________________________________________________________________________
        [Required(ErrorMessage = "لطفا{0}راواردکنید")]
        [MaxLength(50, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شه")]
        [Display(Name = "عنوان گروه")]
        public string GroupTitle { get; set; }
        //_________________________________________________________________________________
        
        
        //_________________________________________________________________________________
        [Display(Name = "  وضعیت نمایش")]
        public bool GroupNotShow { get; set; }
        //_________________________________________________________________________________
        [Display(Name = " (180px*180px)تصویردرمنوکامپیوتر")]
        public string imagepath { get; set; }

        //_________________________________________________________________________________   
    

        //_________________________________________________________________________________
        public virtual ICollection<Product> Products { get; set; }
        //_________________________________________________________________________________
      
    }
}