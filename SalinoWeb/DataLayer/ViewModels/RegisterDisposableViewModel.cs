using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
namespace ViewModels

{
    public class RegisterDisposableViewModel
    {
        [MaxLength(50, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شه")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        [Display(Name = "شماره همراه")]
        [RegularExpression(@"^\(?((\+98|0)?9\d{9})$", ErrorMessage = "لطفا شماره موبایل صحیح را وارد نمایید .")]
        public string RegisterDtoDisposable_Mobile { get; set; }
    }
}