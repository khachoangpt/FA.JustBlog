using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        /// <summary>
        /// Find post by year, month and urlSlug
        /// </summary>
        /// <param name="year">Year of Post</param>
        /// <param name="month">Month of Post</param>
        /// <param name="urlSlug">Url Slug of Post</param>
        /// <returns>Post</returns>
        Task<Post> FindPostAsync(int year, int month, string urlSlug);

        /// <summary>
        /// Get Post was published or unpublished
        /// </summary>
        /// <param name="published">Status of Published</param>
        /// <returns>List of Post</returns>
        Task<IEnumerable<Post>> GetPublisedPostsAsync(bool published = true);

        /// <summary>
        /// Get <paramref name="size"/> Lastest Post
        /// </summary>
        /// <param name="size">number of post</param>
        /// <returns>List of Post</returns>
        IEnumerable<Post> GetLatestPost(int size);

        /// <summary>
        /// Get post by Month
        /// </summary>
        /// <param name="monthYear"></param>
        /// <returns>List of Post</returns>
        Task<IEnumerable<Post>> GetPostsByMonthAsync(DateTime monthYear);

        /// <summary>
        /// Count Post By Category Name
        /// </summary>
        /// <param name="category">Category Name</param>
        /// <returns>int</returns>
        Task<int> CountPostsForCategoryAsync(string category);

        /// <summary>
        /// Get Post By Category Name
        /// </summary>
        /// <param name="category">Category Name</param>
        /// <returns>List of Post</returns>
        Task<IEnumerable<Post>> GetPostsByCategoryAsync(string category);

        /// <summary>
        /// Count Post By Tag Name
        /// </summary>
        /// <param name="tag">Tag Name</param>
        /// <returns>int</returns>
        Task<int> CountPostsForTagAsync(string tag);

        /// <summary>
        /// Get Post By tag name
        /// </summary>
        /// <param name="tag">tag name</param>
        /// <returns>List of Post</returns>
        Task<IEnumerable<Post>> GetPostsByTagAsync(string tag);

        /// <summary>
        /// Get most viewed Posts
        /// </summary>
        /// <param name="size">Number of Posts</param>
        /// <returns>List of Post</returns>
        IEnumerable<Post> GetMostViewedPost(int size);

        /// <summary>
        /// Get highest Posts
        /// </summary>
        /// <param name="size">Number of Posts</param>
        /// <returns>List of Post</returns>
        IEnumerable<Post> GetHighestPosts(int size);
    }
}
