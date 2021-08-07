using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public async Task<int> CountPostsForCategoryAsync(string category)
        {
            return await context.Posts.CountAsync(p => p.Category.Name.Equals(category));
        }

        public async Task<int> CountPostsForTagAsync(string tag)
        {
            return await context.Posts.CountAsync(p => p.Tags.Any(t => t.Name.Equals(tag)));
        }

        public async Task<Post> FindPostAsync(int year, int month, string urlSlug)
        {
            return await context.Posts.FirstOrDefaultAsync(p => p.PostedOn.Year == year
                                             && p.PostedOn.Month == month
                                             && p.UrlSlug.Equals(urlSlug));
        }

        public IEnumerable<Post> GetHighestPosts(int size)
        {
            return context.Posts.OrderByDescending(p => (p.TotalRate / p.RateCount)).Take(size).ToList();
        }

        public IEnumerable<Post> GetLatestPost(int size)
        {
            return context.Posts.OrderByDescending(p => p.PostedOn).Take(size).ToList();
        }

        public IEnumerable<Post> GetMostViewedPost(int size)
        {
            return context.Posts.OrderByDescending(p => p.ViewCount).Take(size).ToList();
        }

        public async Task<IEnumerable<Post>> GetPostsByCategoryAsync(string category)
        {
            return await context.Posts.Where(p => p.Category.Name.Equals(category)).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByMonthAsync(DateTime monthYear)
        {
            return await context.Posts.Where(p => p.PostedOn.Month == monthYear.Month
                                            && p.PostedOn.Year == monthYear.Year).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByTagAsync(string tag)
        {
            return await context.Posts.Where(p => p.Tags.Any(t => t.Name.Equals(tag))).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPublisedPostsAsync(bool published = true)
        {
            return await context.Posts.Where(p => p.Published == published).ToListAsync();
        }
    }
}
