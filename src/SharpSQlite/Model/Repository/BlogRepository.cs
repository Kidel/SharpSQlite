using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpSQlite.Model.Repository
{
    public class BlogRepository
    {
        private readonly DatabaseContext _dbContext = new DatabaseContext();

        public Blog CreateBlog(string name)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    var blog = new Blog { Name = name };
                    _dbContext.Add(blog);
                    _dbContext.SaveChanges();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                    return blog;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public List<Blog> GetBlogList()
        {
            try
            {
                return _dbContext.Blogs.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public Blog GetBlog(int Id)
        {
            try
            {
                return _dbContext.Blogs
                    .Single(b => b.BlogId == Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public Blog UpdateBlog(Blog blog)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    _dbContext.Update(blog);
                    _dbContext.SaveChanges();

                    transaction.Commit();
                    return blog;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public bool DeleteBlog(Blog blog)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    _dbContext.Remove(blog);
                    _dbContext.SaveChanges();

                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
    }
}
