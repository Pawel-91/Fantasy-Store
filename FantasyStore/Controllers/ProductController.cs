using FantasyStore.Models;
using FantasyStore.Models.ViewModels;
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
        public ViewResult List(string category, int productPage = 1) =>
            View(new ProductsListViewModel
            {
                Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products
                        .Count(p => category == null || p.Category == category)
                },
                CurrentCategory = category
            });
    }
}
