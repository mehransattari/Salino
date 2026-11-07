using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.ViewModels
{
    public class ChgangePasswordViewModel
    {

        [Display(Name = "رمزعبورجاری")]
        [Required(ErrorMessage = "لطفا  {0}  راواردکنید")]
        [MaxLength(50, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شود")]
        [DataType(DataType.Password)]
        public string UserOldPassword { get; set; }
        //_________________________________________________________________________
        [Display(Name = "رمزعبورجدید")]
        [Required(ErrorMessage = "لطفا  {0}  راواردکنید")]
        [MaxLength(50, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شود")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        //_________________________________________________________________________
        [Display(Name = "تکرارمرزعبور")]
        [Required(ErrorMessage = "لطفا  {0}  راواردکنید")]
        [MaxLength(50, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شود")]
        [DataType(DataType.Password)]
        [Compare("UserPassword")]
        public string UserRePassword { get; set; }
        //_________________________________________________________________________
    }
}