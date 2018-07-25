using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Storeproducts
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int quantity { get; set; }
        public Double sale_amount { get; set; }
    }
}