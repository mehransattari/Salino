using Salino.ToShamsi;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Salino.Models
{
    public class Entegadat
    {
        public Entegadat()
        {
            Date = DateTime.Now.ToString().Substring(0, 10).ToShamsi();
        }
        [Key]
    
        public int Id { get; set; }
        public int? Parentid { get; set; }
        //______________________________________________________________________
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از {1} کاراکتر وارد شود")]
        [Display(Name = "نام")]
        public string Name { get; set; }
        //______________________________________________________________________
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        [Display(Name = "ایمیل")]
        //[EmailAddress(ErrorMessage = "ایمیل شما نادرست است")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "ایمیل شما نادرست است")]
        public string Email { get; set; }
        //______________________________________________________________________
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        [Display(Name = "متن نظر")]
        [DataType(DataType.MultilineText)]
        public string TextComment { get; set; }
        //______________________________________________________________________      
        [Display(Name = "تاریخ ایجاد")]
        public string Date { get; set; }
        //______________________________________________________________________ 

        [Display(Name = "پاسخ داده شده")]
        public bool OkAnswer { get; set; }
        //______________________________________________________________________



        [ForeignKey("Parentid")]
        public virtual Entegadat Parent { get; set; }
    }
}