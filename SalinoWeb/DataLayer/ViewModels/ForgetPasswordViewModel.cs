using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.ViewModels
{
    public class ForgetPasswordViewModel
    {
        //___________________________________________________________________________
        [MaxLength(50, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شه")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        [Display(Name = "شماره همراه")]
        [RegularExpression(@"^\(?((\+98|0)?9\d{9})$", ErrorMessage = "لطفا شماره موبایل صحیح را وارد نمایید .")]
        public string Mobile { get; set; }

    }
}