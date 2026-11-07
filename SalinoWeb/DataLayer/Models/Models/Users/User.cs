using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Salino.ToShamsi;
namespace Salino.Models
{
    public class User
    {
        public User()
        {
            this.Createdate = DateTime.Now.ToShortDateString().ToShamsi();
        }
        [Key]
        public int Id { get; set; }
        //_________________________________________________________________________________
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Roles { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "نام کامل")]
 
        [MaxLength(50, ErrorMessage = "نباید بیشتر از {1} کاراکتر وارد شه")]
        public string UserName { get; set; }
        //_________________________________________________________________________________ 
         
        [Display(Name = "پسورد")]   
        [MaxLength(50, ErrorMessage = "نباید بیشتر از {1} کاراکتر وارد شه")]
        public string UserPassword { get; set; }

        [Display(Name = "کد یکبارمصرف")]
        [MaxLength(50, ErrorMessage = "نباید بیشتر از {1} کاراکتر وارد شه")]
        public string UserPassword_Disposable { get; set; }

        //_________________________________________________________________________________
        [Display(Name = "شهر ")]
  
        public string cityname { get; set; }

        //_________________________________________________________________________________
        public string ostanname { get; set; }
        [Display(Name = "استان ")]
        public int OstanId { get; set; }
        [ForeignKey("OstanId")]
        public virtual Ostan Ostans { get; set; }

        //_______________________________________________________________________________
        [DataType(DataType.PostalCode)]     
        [Display(Name = "کد پستی")]
        [MaxLength(50, ErrorMessage = "نباید بیشتر از {1} کاراکتر وارد شه")]
        public string PostalCode { get; set; }

        //_________________________________________________________________________________
     
        [DataType(DataType.MultilineText)]

        [Display(Name = "آدرس")]
        public string StreetAddress { get; set; }

        //_________________________________________________________________________________
        [MaxLength(50, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شه")]

        [Display(Name = "شماره همراه")]
        [RegularExpression(@"^\(?((\+98|0)?9\d{9})$", ErrorMessage ="لطفا شماره موبایل صحیح را وارد نمایید .")]
        public string Mobile { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "فعال")]
        public bool IsActive { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "تاریخ عضویت")]
        public string Createdate { get; set; }
        //_________________________________________________________________________________

        [Display(Name = "کدمنحصربه فرد")]
        public string CodeActivate { get; set; }
        //_________________________________________________________________________________
     
       
        public virtual  ICollection<FactorMain> FactorMain { get; set; }
        //_________________________________________________________________________________


    }
}