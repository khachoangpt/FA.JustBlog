using FA.JustBlog.Core.Repositories;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlog.WebMVC.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostController()
        {
            _postRepository = new PostRepository();
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var posts = await _postRepository.GetAllAsync();
            return View(posts);
        }

        public async Task<ActionResult> Detail(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            return View(post);
        }

        public async Task<ActionResult> Details(int year, int month, string urlSlug)
        {
            var post = await _postRepository.FindPostAsync(year, month, urlSlug);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View("Detail", post);
        }

        public ActionResult LastestPost()
        {
            var posts = _postRepository.GetLatestPost(3);
            return PartialView("_LastestPost", posts);
        }

        public ActionResult MostViewedPost()
        {
            var posts = _postRepository.GetMostViewedPost(5);
            return PartialView("_MostViewedPost", posts);
        }

        public async Task<ActionResult> GetPostByCategory(string category)
        {
            var posts = await _postRepository.GetPostsByCategoryAsync(category);
            return View(posts);
        }

        public async Task<ActionResult> GetPostByTag(string tag)
        {
            var posts = await _postRepository.GetPostsByTagAsync(tag);
            return View(posts);
        }
    }
}