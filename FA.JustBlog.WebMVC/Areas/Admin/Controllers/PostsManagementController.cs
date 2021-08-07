using FA.JustBlog.Core.Data;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using FA.JustBlog.WebMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlog.WebMVC.Areas.Admin.Controllers
{
    public class PostsManagementController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;

        public PostsManagementController()
        {
            _postRepository = new PostRepository();
            _categoryRepository = new CategoryRepository();
            _tagRepository = new TagRepository();
        }

        public async Task<ActionResult> Index()
        {
            var posts = await _postRepository.GetAllAsync();
            return View(posts);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            var postViewModel = new PostViewModel
            {
                Tags = _tagRepository.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
            };
            return View(postViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    Id = Guid.NewGuid(),
                    Title = postViewModel.Title,
                    UrlSlug = postViewModel.UrlSlug,
                    ShortDescription = postViewModel.ShortDescription,
                    ImageUrl = postViewModel.ImageUrl,
                    PostContent = postViewModel.PostContent,
                    Published = postViewModel.Published,
                    ViewCount = postViewModel.ViewCount,
                    RateCount = postViewModel.RateCount,
                    TotalRate = postViewModel.TotalRate,
                    CategoryId = postViewModel.CategoryId,
                    Tags = await GetSelectedTagFromIds(postViewModel.SelectedTagIds)
                };
            }

            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name", postViewModel.CategoryId);
            postViewModel.Tags = _tagRepository.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            return View(postViewModel);
        }

        private async Task<ICollection<Tag>> GetSelectedTagFromIds(IEnumerable<Guid> selectedTagIds)
        {
            var tags = new List<Tag>();

            var tagEntities = await _tagRepository.GetAllAsync();

            foreach (var item in tagEntities)
            {
                if (selectedTagIds.Any(x => x == item.Id))
                {
                    tags.Add(item);
                }
            }
            return tags;
        }

        public async Task<ActionResult> Edit(Guid? id)
        {
            var post = await _postRepository.GetByIdAsync((Guid)id);
            var postViewModel = new PostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                UrlSlug = post.UrlSlug,
                ShortDescription = post.ShortDescription,
                ImageUrl = post.ImageUrl,
                PostContent = post.PostContent,
                Published = post.Published,
                ViewCount = post.ViewCount,
                RateCount = post.RateCount,
                TotalRate = post.TotalRate,
                CategoryId = post.CategoryId,
            };
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name", postViewModel.CategoryId);
            postViewModel.Tags = _tagRepository.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            ViewBag.TagList = _tagRepository.GetAll();
            return View(postViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var post = await _postRepository.GetByIdAsync(postViewModel.Id);
                if (post == null)
                {
                    return HttpNotFound();
                }

                post.Title = postViewModel.Title;
                post.UrlSlug = postViewModel.UrlSlug;
                post.ShortDescription = postViewModel.ShortDescription;
                post.ImageUrl = postViewModel.ImageUrl;
                post.PostContent = postViewModel.PostContent;
                post.Published = postViewModel.Published;
                post.RateCount = postViewModel.RateCount;
                post.TotalRate = postViewModel.TotalRate;
                post.ViewCount = postViewModel.ViewCount;
                post.CategoryId = postViewModel.CategoryId;
                await UpdateSelectedTagFromIds(postViewModel.SelectedTagIds, post);

                var result = await _postRepository.UpdateAsync(post);
                if (result)
                {
                    TempData["Message"] = "Update successful!";
                }
                else
                {
                    TempData["Message"] = "Update failed!";
                }
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name", postViewModel.CategoryId);
            postViewModel.Tags = _tagRepository.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            ViewBag.TagList = _tagRepository.GetAll();

            return View(postViewModel);
        }

        private async Task UpdateSelectedTagFromIds(IEnumerable<Guid> selectedTagIds, Post post)
        {
            var tags = post.Tags;
            foreach (var item in tags.ToList())
            {
                post.Tags.Remove(item);
            }
            post.Tags = await GetSelectedTagFromIds(selectedTagIds);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            var result = await _postRepository.DeleteAsync((Guid)id);
            if (result)
            {
                TempData["Message"] = "Delete Successful";
            }
            else
            {
                TempData["Message"] = "Delete failed";
            }
            return RedirectToAction("Index");
        }
    }
}
