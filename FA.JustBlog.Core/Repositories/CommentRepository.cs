using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public IList<Comment> GetCommentsForPost(Guid postId)
        {
            return context.Comments.Where(c => c.PostId == postId).ToList();
        }

        public IList<Comment> GetCommentsForPost(Post post)
        {
            return context.Comments.Where(c => c.PostId == post.Id).ToList();
        }

        public async Task<IList<Comment>> GetCommentsForPostAsync(Guid postId)
        {
            return await context.Comments.Where(c => c.PostId == postId).ToListAsync();
        }

        public async Task<IList<Comment>> GetCommentsForPostAsync(Post post)
        {
            return await context.Comments.Where(c => c.PostId == post.Id).ToListAsync();
        }
    }
}
