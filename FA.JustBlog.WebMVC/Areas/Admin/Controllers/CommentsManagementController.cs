using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using FA.JustBlog.WebMVC.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlog.WebMVC.Areas.Admin.Controllers
{
    public class CommentsManagementController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;

        public CommentsManagementController()
        {
            _commentRepository = new CommentRepository();
            _postRepository = new PostRepository();
        }

        public async Task<ActionResult> Index()
        {
            var posts = await _commentRepository.GetAllAsync();
            return View(posts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Id = Guid.NewGuid(),
                    Name = commentViewModel.Name,
                    Email = commentViewModel.Email,
                    PostId = commentViewModel.PostId,
                    CommentHeader = commentViewModel.CommentHeader,
                    CommentText = commentViewModel.CommentText,
                    CommentTime = commentViewModel.CommentTime
                };
                var result = await _commentRepository.AddAsync(comment);
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

            return View(commentViewModel);
        }

        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comment = await _commentRepository.GetByIdAsync((Guid)id);
            var commentViewModel = new CommentViewModel()
            {
                Id = comment.Id,
                Name = comment.Name,
                Email = comment.Email,
                PostId = comment.PostId,
                CommentHeader = comment.CommentHeader,
                CommentText = comment.CommentText,
                CommentTime = comment.CommentTime
            };

            return View(commentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(CommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                var comment = await _commentRepository.GetByIdAsync(commentViewModel.Id);
                if (comment == null)
                {
                    return HttpNotFound();
                }

                comment.Name = commentViewModel.Name;
                comment.Email = commentViewModel.Email;
                comment.PostId = commentViewModel.PostId;
                comment.CommentHeader = commentViewModel.CommentHeader;
                comment.CommentText = commentViewModel.CommentText;
                comment.CommentTime = commentViewModel.CommentTime;

                var result = await _commentRepository.UpdateAsync(comment);
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
            return View(commentViewModel);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            var result = await _commentRepository.DeleteAsync((Guid)id);
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
