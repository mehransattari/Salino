using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "نام نقش")]
        [Required(ErrorMessage = "لطفا{0}راواردکنید")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شه")]
        public string RoleName { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا{0}راواردکنید")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شه")]
        public string RoleTitle { get; set; }
        //_________________________________________________________________________________

       
    }
}