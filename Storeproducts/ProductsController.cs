using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Storeproducts
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]
         {
            new Product { Id = 1, Name = "Sugar", quantity = 100, sale_amount = 1643.57 },
            new Product { Id = 2, Name = "Tea", quantity = 200, sale_amount = 1643.67 },
            new Product { Id = 3, Name = "Cofee", quantity = 300, sale_amount = 1643.77 }
         };
        // GET api/<controller>
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        // GET api/<controller>/5
        public Product GetProductById(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

        // POST api/<controller>
        public IEnumerable<Product> GetProductsByCategory(string name)
        {
            return products.Where(
                (p) => string.Equals(p.Name, name,
                    StringComparison.OrdinalIgnoreCase));
        }        
    }
}