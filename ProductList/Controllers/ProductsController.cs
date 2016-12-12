using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProductList.Models;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace ProductList.Controllers
{
    public class ProductsController : Controller
    {
        private ProductContext db = new ProductContext();

        // Products
        public ActionResult Index(string category, string keyword)
        {
            var categories = new List<string>();
            var query = from p in db.Products orderby p.Category select p.Category;
            categories.AddRange(query.Distinct());
            ViewBag.category = new SelectList(categories);
            var products = from p in db.Products select p;
            if (!String.IsNullOrEmpty(keyword))
            {
                products = products.Where(p => p.ProductCode.Contains(keyword) || p.ProductName.Contains(keyword));
            }
            if (!String.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category == category);
            }
            return View(products);
        }
                

        // GET: Products/Details/productCode
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);            
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(Product.CategoryTypes);
            ViewBag.Unit = new SelectList(Product.UnitTypes);
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCode,ProductName,Category,Unit,ProductPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/productCode
        public ActionResult Edit(string id)
        {
            ViewBag.Category = new SelectList(Product.CategoryTypes);
            ViewBag.Unit = new SelectList(Product.UnitTypes);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);           
        }

        // POST: Products/Edit/productCode
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCode,ProductName,Category,Unit,ProductPrice")] Product product)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/productCode
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //export product list to excel file
        public ActionResult ExportToExcel()
        {
            GridView gv = new GridView();
            gv.DataSource = db.Products.ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Productlist.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Products");
        }
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
