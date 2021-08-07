using FA.JustBlog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FA.JustBlog.WebMVC.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public TagController()
        {
            _tagRepository = new TagRepository();
        }

        // GET: Tag
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PopularTags()
        {
            var tags = _tagRepository.GetAll().Take(10);
            return PartialView("_PopularTags", tags);
        }
    }
}