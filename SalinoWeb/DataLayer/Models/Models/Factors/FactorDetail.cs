using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Salino.Models
{
    public class FactorDetail
    {
        [Key]
        public int Id { get; set; }
        //_______________________________________________________________________
        [Display(Name = "فاکتوراصلی")]
        public int FactorMainId { get; set; }
        //_______________________________________________________________________
        public int ProductId { get; set; }
        [Display(Name = "نام کالا")]
        public string ProductName { get; set; }
        //_______________________________________________________________________
        public int FarbicTypeId { get; set; }
        [Display(Name = "نوع پارچه")]
        public string FarbicTypeName { get; set; }
        //_______________________________________________________________________

        [Display(Name = "تعداد")]
        public int DetailCount { get; set; }
        //_______________________________________________________________________

        [Display(Name = "مبلغ واحد")]
        public int DetailPrice { get; set; }
        //_______________________________________________________________________

        [Display(Name = "مبلغ کل")]
        public int SumPrice { get; set; }
        //_______________________________________________________________________


        [ForeignKey("FactorMainId")]
        public virtual FactorMain FactorMain { get; set; }
        //_______________________________________________________________________
    }
}