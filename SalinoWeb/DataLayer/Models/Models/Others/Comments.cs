using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Salino.ToShamsi;

namespace Salino.Models
{
    public class Comments
    {
        public Comments()
        {
            this.Createdate = DateTime.Now.ToShortDateString().ToShamsi();
        }
        [Key]
        public int Id { get; set; }
        //______________________________________________________________________
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product product { get; set; }
        //______________________________________________________________________
    
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Comments Parent { get; set; }
        //______________________________________________________________________
        [Required(ErrorMessage = "لطفا{0}راواردکنید")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شود")]
        [Display(Name = "نام")]
        public string Name { get; set; }
        //______________________________________________________________________
        //[Required(ErrorMessage = "لطفا{0}راواردکنید")] 
        //[Display(Name = "ایمیل")]
        //[EmailAddress(ErrorMessage = "ایمیل شما نادرست است")]
        ////[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "ایمیل شما نادرست است")]
        //public string Email { get; set; }
        //______________________________________________________________________
        [Required(ErrorMessage = "لطفا{0}راواردکنید")] 
        [Display(Name = "متن نظر")]
        [DataType(DataType.MultilineText)]
        public string  TextComment { get; set; }
        //______________________________________________________________________      
        [Display(Name = "تاریخ ایجاد")]        
        public string Createdate { get; set; }
        //______________________________________________________________________ 
        [Display(Name = "فعال")]
        public bool IsShow { get; set; }
        //______________________________________________________________________ 
        [Display(Name = "پاسخ داده شده")]
        public bool OkAnswer { get; set; }
        //______________________________________________________________________
      
     

    }

}