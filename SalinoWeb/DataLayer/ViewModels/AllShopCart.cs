using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.ViewModels
{
    public class AllShopCart
    {
        public int sumAllPrices { get; set; } = 0;
        public int CountAllShopCart { get; set; } = 0;
    }

}