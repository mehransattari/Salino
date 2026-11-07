using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
namespace ViewModels
{
    public class LoginDisposableViewModel
    {
        [MaxLength(50, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شه")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        [Display(Name = "رمز عبور")]
        public string LoginDtoDisposable_Password { get; set; }
        public string LoginDtoDisposable_Mobile { get; set; }
    }
}