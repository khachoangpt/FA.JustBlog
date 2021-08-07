using FA.JustBlog.Core.Repositories;
using System.Web.Mvc;

namespace FA.JustBlog.WebMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController()
        {
            _categoryRepository = new CategoryRepository();
        }

        // GET: Category
        public ActionResult Index()
        {
            var categories = _categoryRepository.GetAll();
            return PartialView("_MenuCategory", categories);
        }
    }
}