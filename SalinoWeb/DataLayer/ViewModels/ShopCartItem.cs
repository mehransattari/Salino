using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Salino.ViewModels
{
    public class ShopCartItem
    {
        public int ProductID { get; set; }
        public string CodeKala { get; set; }
        public int GroupId { get; set; }
        public int FarbictypeId { get; set; }
        public string Imgpath { get; set; }
        public int FarbicTypeProductId { get; set; }

        public int Count { get; set; }
        public int Farbictypeprice { get; set; }
        public int sumproduct { get; set; }
        public int colorId { get; set; }

    }
}