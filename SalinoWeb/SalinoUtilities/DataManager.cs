using Salino.DataLayer;
using Salino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SalinoMvc5.Controllers
{
    public static class DataManager
    {
      
        /// <summary>
        /// Returns one block of book items
        /// </summary>
        /// <param name="BlockNumber">Starting from 1</param>
        /// <param name="BlockSize">Items count in a block</param>
        /// <returns></returns>
        public static List<Product> GetProducts(int BlockNumber, int BlockSize,int id)
        {
            SalinoContext db = new SalinoContext();
            int startIndex = (BlockNumber - 1) * BlockSize;           
            var product= db.Products.Where(x => x.GroupId == id).OrderBy(x=>x.CreateDate).Skip(startIndex).Take(BlockSize).ToList();
            return product;
        }
    }

  

    public class JsonModel
    {
        public string HTMLString { get; set; }
        public bool NoMoreData { get; set; }
    }
}