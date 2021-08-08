using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        /// <summary>
        /// Get Comment by ID of Post
        /// </summary>
        /// <param name="postId">ID of Post</param>
        /// <returns>List of Comment</returns>
        IList<Comment> GetCommentsForPost(Guid postId);

        /// <summary>
        /// Get Comment by Post
        /// </summary>
        /// <param name="post">Post object</param>
        /// <returns>List of Comment</returns>
        IList<Comment> GetCommentsForPost(Post post);

        /// <summary>
        /// Get Comment by ID of Post
        /// </summary>
        /// <param name="postId">ID of Post</param>
        /// <returns>List of Comment</returns>
        Task<IList<Comment>> GetCommentsForPostAsync(Guid postId);

        /// <summary>
        /// Get Comment by Post
        /// </summary>
        /// <param name="post">Post object</param>
        /// <returns>List of Comment</returns>
        Task<IList<Comment>> GetCommentsForPostAsync(Post post);
    }
}
