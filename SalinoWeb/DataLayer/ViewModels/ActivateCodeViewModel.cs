using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.ViewModels
{
    public class ActivateCodeViewModel
    {
        [Display(Name = "کدفعالساز")]
        [MaxLength(10, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شه")]
        public string UserCode { get; set; }
    
    }
}