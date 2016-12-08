using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProductList.Models;
using System.Web.Http;
using System.Web.Routing;

namespace ProductList.Controllers
{
    [RoutePrefix("products")]
    public class ProductsApiController : ApiController
    {
        private ProductContext db = new ProductContext();

        // GET: products/All
        [Route("all")]
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        // GET: products/productCode/GF250
        //[ResponseType(typeof(Product))]
        //[Route("productCode/{productCode}")]
        //public IHttpActionResult GetProduct(string productCode)
        //{
        //    Product product = db.Products.Find(productCode);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(product);
        //}

        // GET: products/productCode/GF250
        [ResponseType(typeof(Product))]
        [Route("productCode/{productCode}")]
        public IHttpActionResult GetProduct(string productCode)
        {
            var product = db.Products.Where(p=>p.ProductCode.Contains(productCode.ToUpper())).OrderBy(p => p.ProductCode);
            if (product.Count()>0)
            {
                return Ok(product.ToList());
            }
            else
            {
                return NotFound();
            }
            
        }


        // GET: products/category/electrical
        [ResponseType(typeof(Product))]
        [Route("category/{category}")]
        public IHttpActionResult GetProductsInCategory(string category)
        {
            var productsInCategory = db.Products.Where(p => p.Category.Contains(category.ToUpper())).OrderBy(p => p.ProductCode);
            if (productsInCategory.Count()>0)
            {
                return Ok(productsInCategory);
            }
            else
            {
                return NotFound();             
            }
        }

        // GET: products/productName/keyword
        [ResponseType(typeof(Product))]
        [Route("productName")]
        public IHttpActionResult GetProductsByKeyword(string keyword)
        {
            var query = db.Products.Where(p => p.ProductName.Contains(keyword.ToUpper())).OrderBy(p => p.ProductCode);
            if (query.Count() >0)
            {
                return Ok(query.ToList());
                
            }
            else
            {
                return NotFound();
            }
        }







        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(string id)
        {
            return db.Products.Count(e => e.ProductCode == id) > 0;
        }
    }
}