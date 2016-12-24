using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpSQlite.Model.Repository
{
    public class PostAggregate
    {
        private readonly DatabaseContext _dbContext = new DatabaseContext();

        public Post CreatePost(string title, string slug, string content, List<string> tags, int blogId, int authorId)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    var post = new Post { Title = title, Slug = slug, Content = content, BlogId = blogId, AuthorId = authorId };
                    _dbContext.Add(post);

                    foreach (string tagName in tags)
                    {
                        Tag tag = new Tag { Name = tagName };
                        _dbContext.Add(tag);
                        var postTag = new PostTag { Tag = tag, Post = post };
                        _dbContext.Add(postTag);
                    }

                    _dbContext.SaveChanges();
                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                    return post;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public List<Post> GetPostList()
        {
            try
            {
                return _dbContext.Posts.Include(post => post.Blog).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public List<Post> GetPostListByBlog(Blog blog)
        {
            try
            {
                return _dbContext.Posts
                    .Include(post => post.Blog)
                    .Where(p => p.BlogId == blog.BlogId)
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public List<Post> GetPostListByAuthor(User author)
        {
            try
            {
                return _dbContext.Posts
                    .Include(post => post.Blog)
                    .Include(post => post.Author)
                    .Where(p => p.AuthorId == author.UserId)
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public Post GetPost(int Id)
        {
            try
            {
                return _dbContext.Posts
                    .Include(post => post.Blog)
                    .Include(post => post.Author)
                    .Single(p => p.PostId == Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public Post GetMostRecentPost()
        {
            try
            {
                return _dbContext.Posts
                    .Include(post => post.Blog)
                    .Include(post => post.Author)
                    .Last();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public Post UpdatePost(Post post)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    _dbContext.Update(post);
                    _dbContext.SaveChanges();

                    transaction.Commit();
                    return post;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public Post DeletePost(Post post)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    _dbContext.Remove(post);
                    _dbContext.SaveChanges();

                    transaction.Commit();
                    return post;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public Post AddUserToContributors(Post post, int contributorId)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    post.ContributorId = contributorId;
                    _dbContext.Update(post);
                    _dbContext.SaveChanges();

                    transaction.Commit();
                    return post;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public Post AddTagToPost(Post post, string tagName)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    Tag tag = new Tag { Name = tagName };
                    _dbContext.Add(tag);
                    var postTag = new PostTag { Tag = tag, PostId = post.PostId };
                    _dbContext.Add(postTag);
                    _dbContext.SaveChanges();

                    transaction.Commit();
                    return post;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public Post AddExistingTagToPost(Post post, int tagId)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    var postTag = new PostTag { TagId = tagId, PostId = post.PostId };
                    _dbContext.Add(postTag);
                    _dbContext.SaveChanges();

                    transaction.Commit();
                    return post;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
