using FantasyStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FantasyStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index() => View(repository.Products);

        public ViewResult Edit(int productId)
            => View(repository.Products
                .FirstOrDefault(p => p.ProductID == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"Saved {product.Name}";
                return RedirectToAction("Index");
            }
            else
            {
                // Error in data
                return View(product);
            }
        }

        public ViewResult Create()
            => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if(deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} has been deleted.";
            }
            return RedirectToAction("Index");
        }
    }
}