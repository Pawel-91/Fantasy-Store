using FantasyStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FantasyStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        // GET: /<controller>/
        public ViewResult List(int ProductPage = 1) =>
            View(repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((ProductPage - 1) * PageSize)
                .Take(PageSize));
    }
}
