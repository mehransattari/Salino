using Salino.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalinoMvc5.Areas.Admin.Models.ViewModel
{
    public class PageListViewModel
    {
        public IEnumerable<Product> Product { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItemCount { get; set; }

    }
}