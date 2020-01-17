using FantasyStore.Models;
using Microsoft.AspNetCore.Mvc;

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

    }
}