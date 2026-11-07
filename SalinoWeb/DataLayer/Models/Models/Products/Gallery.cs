using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Salino.Models
{
    public class Gallery
    {
        [Key]
        public int Id { get; set; }
        //____________________________________________________________________

        public int ProductId { get; set; }
        //____________________________________________________________________

        [Display(Name = "  تصویر گالری")]
        [MaxLength(100, ErrorMessage = "نباید بیش تر از {1} کاراکتر باشد")]
        public string ImgPath { get; set; }
        //____________________________________________________________________      

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}