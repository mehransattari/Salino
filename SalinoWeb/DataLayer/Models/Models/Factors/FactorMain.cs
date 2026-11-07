using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using Salino.DataLayer;
using Salino.ToShamsi;

namespace Salino.Models
{
    //
    public class FactorMain
    {
        public FactorMain()
        {
          
            Random random = new Random();
          
            this.CodeGenerator = random.Next(1000, 10000);
        


        }

        //==============================================================//
        [Key]
        public int Id { get; set; }
        //==============================================================//

        [Display(Name = "نوع مشتری")]
        public int RoleId { get; set; }
        //==============================================================//
        [Display(Name = "کد مرسوله")]
        public long CodeGenerator { get; set; }
        //==============================================================//
        [Display(Name = "وضعیت پرداخت")]
        public string Paymentstatus { get; set; }
        //==============================================================//
        [Display(Name = "شماره پرداخت ")]
        [MaxLength(50)]
        public string  PaymentNumber { get; set; }
        //==============================================================//
        [Display(Name = "شماره فاکتور")]
        public long FactorNumber { get; set; }
        //==============================================================//
        [Display(Name = "شماره کارت بانکی")]
        public string CardNumberMasked { get; set; }
        //==============================================================//
        [Display(Name = "شماره مرجع")]
        public string RRN { get; set; }
        //==============================================================//



        #region Noimport
        //اینا مهم نیستن
        [Display(Name = "  	شماره پیگیری پست ")]
        [MaxLength(150)]
        public string PostNumber { get; set; }
        //_____________________________________________________________________ 

        [Display(Name = "شماره پرداخت")]
        [MaxLength(50)]
        public string SaleReferenceId { get; set; }
        //_____________________________________________________________________
        #endregion

        #region Price

        [Display(Name = "مبلغ")]
        public int Amount { get; set; }
        //_____________________________________________________________________
        [Display(Name = "تخفیف")]
        public int Discount { get; set; }
        //_______________________________________________________________________


        [Display(Name = "مبلغ نهایی")]
        public int SumAllPrice { get; set; }
        //_______________________________________________________________________
        [Display(Name = "پرداخت شده")]
        public bool IsPay { get; set; }
        //_____________________________________________________________________
        #endregion

        #region ReceiverUser

        [Display(Name = "آی دی گیرنده")]
        public int UserId { get; set; }
   
        [Display(Name = "نام گیرنده")]
        public string Username { get; set; }
      
        [Display(Name = "شماره تماس گیرنده")]
        public string UserMobile { get; set; }

        [Display(Name = "کدپستی")]
        public string UserCodePosti { get; set; }

        [Display(Name = "استان")]
        public string UserOstan { get; set; }

        [Display(Name = "شهر")]
        public string UserCity { get; set; }

        [Display(Name = "آدرس تحویل گیرنده")]
        [DataType(DataType.MultilineText)]
        public string AddressUser { get; set; }

        #endregion

        #region Date

        //_____________________________________________________________________
        [Display(Name = "تاریخ خرید")]
        public string DateCreate { get; set; }
        [Display(Name = "زمان خرید")]
        public string Time { get; set; }
        public DateTime DateCreateDatetime { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        //_____________________________________________________________________
        [Display(Name = "زمان تحویل")]
        public string Deliverytime { get; set; }
        //_____________________________________________________________________

        [Display(Name = "وضعیت سفارش")]
        public  StatusAllTypeFactorsEnum StatusAll { get; set; }
        //_____________________________________________________________________
        #endregion

        #region TypeSend

        [Display(Name = " نوع ارسال")]
        public int TypeSendPostId { get; set; }
        [Display(Name = " نام ارسال")]
        public string TypeSendPostName { get; set; }

        //_____________________________________________________________________



        #endregion



        public virtual ICollection<FactorDetail> FactorDetails { get; set; }
    }
    public enum StatusAllTypeFactorsEnum
    {
        [Display(Name = "درحال بررسی ")]
        Pending = 1,

        [Display(Name = "درحال آماده سازی")]
        Preparing = 2,

        [Display(Name = "در حال ارسال")]
        Sending = 3,

        [Display(Name = "تکمیل ارسال")]
        CompleteSubmission = 4,

        [Display(Name = "مرجوعی")]
        Returns = 5,
        [Display(Name = "ارور")]
        error =6
    }
}