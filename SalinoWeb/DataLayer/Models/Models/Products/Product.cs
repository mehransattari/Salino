using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Salino.ToShamsi;
namespace Salino.Models
{
    public class Product
    {
        public Product()
        {
            this.CreateDate = DateTime.Now.ToShortDateString().ToShamsi();
            this.CreateTime = DateTime.Now.ToShortTimeString();
            this.CreateDateEnglish = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        //_________________________________________________________________________________
        [Display(Name = "کد محصول")]      
        [MaxLength(100, ErrorMessage = "نباید بیش تر از {1} کاراکتر باشد")]
        public string CodeKala { get; set; }
       
 
        //_________________________________________________________________________________
        [Display(Name = "گروه")]
        public int GroupId { get; set; }
     
        //_________________________________________________________________________________
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "مقداری برای {0} وارد نمایید")]
        [MaxLength(100, ErrorMessage = "نباید بیش تر از {1} کاراکتر باشد")]
        public string Name { get; set; }
        //_________________________________________________________________________________
        [Display(Name = " (553.5px*553.5px) تصویرجزئیات محصول")]       
        public string Imgdetail { get; set; }
        //_________________________________________________________________________________  
        [Display(Name = "   (1847px*184px)تصویرجزئیات در موبایل   ")]
        public string ImgdetailMob { get; set; }
        //_________________________________________________________________________________     
        [Display(Name = "(320px*320px)تصویراصلی در صفحه اصلی")]      
        public string ImgMain { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "  (95px*95px) تصویراصلی در  موبایل")]
        public string ImgMainMob { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "توضیحات محصول")]
        [DataType(DataType.MultilineText)]
        
        public string Description { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "بازدید")]
        public int Seen { get; set; }    
       
        //_________________________________________________________________________________
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }

        //_________________________________________________________________________________

        [Display(Name = " وضعیت نمایش")]
        public bool IsShow { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "رنگ محصول")]
        public int? ColorId { get; set; }
        public virtual Color Color { get; set; }
        //_________________________________________________________________________________
        public DateTime CreateDateEnglish { get; set; }
        [ForeignKey("GroupId")]
        public virtual GroupProduct GroupProduct { get; set; }
        //_________________________________________________________________________________ 
        public virtual ICollection<FarbicType> FarbicType { get; set; }
        //_________________________________________________________________________________
        public virtual ICollection<Monasebat> Monasebats { get; set; }
        //_________________________________________________________________________________







    }
}