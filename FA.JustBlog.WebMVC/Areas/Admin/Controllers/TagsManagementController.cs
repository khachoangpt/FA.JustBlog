using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using FA.JustBlog.WebMVC.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlog.WebMVC.Areas.Admin.Controllers
{
    public class TagsManagementController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public TagsManagementController()
        {
            _tagRepository = new TagRepository();
        }

        public async Task<ActionResult> Index()
        {
            var posts = await _tagRepository.GetAllAsync();
            return View(posts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = tagViewModel.Name,
                    UrlSlug = tagViewModel.UrlSlug,
                    Description = tagViewModel.Description,
                    Count = tagViewModel.Count
                };
                var result = await _tagRepository.AddAsync(tag);
                if (result > 0)
                {
                    TempData["Message"] = "Insert successful!";
                }
                else
                {
                    TempData["Message"] = "Insert failed!";
                }
                return RedirectToAction("Index");
            }

            return View(tagViewModel);
        }

        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tag = await _tagRepository.GetByIdAsync((Guid)id);
            var tagViewModel = new TagViewModel()
            {
                Id = tag.Id,
                Name = tag.Name,
                UrlSlug = tag.UrlSlug,
                Description = tag.Description,
                Count = tag.Count
            };

            return View(tagViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                var tag = await _tagRepository.GetByIdAsync(tagViewModel.Id);
                if (tag == null)
                {
                    return HttpNotFound();
                }

                tag.Name = tagViewModel.Name;
                tag.UrlSlug = tagViewModel.UrlSlug;
                tag.Description = tagViewModel.Description;
                tag.Count = tagViewModel.Count;

                var result = await _tagRepository.UpdateAsync(tag);
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
            return View(tagViewModel);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            var result = await _tagRepository.DeleteAsync((Guid)id);
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
