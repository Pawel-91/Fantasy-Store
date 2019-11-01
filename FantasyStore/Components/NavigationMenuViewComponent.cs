using FantasyStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FantasyStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IProductRepository repository;

        public NavigationMenuViewComponent(IProductRepository productRepository)
        {
            repository = productRepository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.CurrentCategory = RouteData?.Values["category"];
            return View(repository.Products
                .Select(prod => prod.Category)
                .Distinct()
                .OrderBy(cat => cat));
        }
    }
}
