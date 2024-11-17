using MachineTestApp.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;



    namespace MachineTestApp.Controllers
    {
        public class ProductController : Controller
        {
            private readonly AppDbContext _context = new AppDbContext();

            // Index
            public ActionResult Index()
            {
                var products = _context.Products.Include("Category").ToList();
                return View(products);
            }

        // Create (GET)
        public ActionResult Create()
        {
            var categories = _context.Categories
                                     .Select(c => new SelectListItem
                                     {
                                         Value = c.CategoryId.ToString(),
                                         Text = c.CategoryName
                                     }).ToList();

            return View(new Product { Categories = categories });
        }


        // Create (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            product.Categories = _context.Categories
                                         .Select(c => new SelectListItem
                                         {
                                             Value = c.CategoryId.ToString(),
                                             Text = c.CategoryName
                                         }).ToList();

            return View(product);
        }


        // Edit (GET)
        public ActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return HttpNotFound();

            product.Categories = _context.Categories
                                         .Select(c => new SelectListItem
                                         {
                                             Value = c.CategoryId.ToString(),
                                             Text = c.CategoryName,
                                             Selected = c.CategoryId == product.CategoryId
                                         }).ToList();

            return View(product);
        }

        // Edit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            product.Categories = _context.Categories
                                         .Select(c => new SelectListItem
                                         {
                                             Value = c.CategoryId.ToString(),
                                             Text = c.CategoryName,
                                             Selected = c.CategoryId == product.CategoryId
                                         }).ToList();

            return View(product);
        }


        // Delete (GET)
        public ActionResult Delete(int id)
            {
                var product = _context.Products.Include("Category").FirstOrDefault(p => p.ProductId == id);
                if (product == null) return HttpNotFound();
                return View(product);
            }

            // Delete (POST)
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                var product = _context.Products.Find(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
