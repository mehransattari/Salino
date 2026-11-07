using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Salino.ToShamsi;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salino.Models
{
    public class Ticket
    {
        public Ticket()
        {
            this.Createdate = DateTime.Now.ToShortDateString().ToShamsi();
        }
        //_________________________________________________________________________________
        [Key]
        public int Id { get; set; }

        //_________________________________________________________________________________
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        //_________________________________________________________________________________
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Ticket Parent { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "موضوع")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
        [MaxLength(50, ErrorMessage = "نباید بیشتر از {1} کاراکتر وارد شود")]
        public string TitleTicket { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} راواردکنید")]
         [DataType(DataType.MultilineText)]
        public string TextTicket { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "تاریخ عضویت")]
        public string Createdate { get; set; }
        //_________________________________________________________________________________
        [Display(Name = "پاسخ داده شده")]
        public bool OkAnswer { get; set; }
    }
}