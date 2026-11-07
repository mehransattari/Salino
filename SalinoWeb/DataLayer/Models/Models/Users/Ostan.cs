using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.Models
{
    public class Ostan
    {
        [Key]

        public int Id { get; set; }
        public int OstanCode { get; set; }
        [Required(ErrorMessage = "لطفا{0}راواردکنید")]
        [Display(Name = "استان")]
        [MaxLength(50, ErrorMessage = "نباید بیشتر از{1}کاراکتر وارد شود")]
        public string ostanname { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}