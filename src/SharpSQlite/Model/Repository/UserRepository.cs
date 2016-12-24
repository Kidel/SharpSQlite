using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpSQlite.Model.Repository
{
    public class UserRepository
    {
        private readonly DatabaseContext _dbContext = new DatabaseContext();

        public User CreateUser(string email, string firstName, string lastName)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    var user = new User { Email = email, FirstName = firstName, LastName = lastName };
                    _dbContext.Add(user);
                    _dbContext.SaveChanges();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                    return user;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public List<User> GetUserList()
        {
            try
            {
                return _dbContext.Users.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public User GetUser(int Id)
        {
            try
            {
                return _dbContext.Users
                    .Single(b => b.UserId == Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                return _dbContext.Users
                    .Single(b => b.Email == email);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public User UpdateUser(User user)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    _dbContext.Update(user);
                    _dbContext.SaveChanges();

                    transaction.Commit();
                    return user;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public bool DeleteUser(User user)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    _dbContext.Remove(user);
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
