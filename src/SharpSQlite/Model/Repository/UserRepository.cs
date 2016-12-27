using SharpSQlite.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpSQlite.Model.Repository
{
    public class UserRepository
    {
        private readonly DatabaseContext _dbContext = new DatabaseContext();

        public User CreateUser(string email, string firstName, string lastName, string dateOfBirth, string password, string secretQuestion)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var salt = Hash.GenerateSalt();
                var hashPassword = Hash.HashString(password, salt);
                var user = new User { Email = email, FirstName = firstName, LastName = lastName, DateOfBirth = Convert.ToDateTime(dateOfBirth),
                                      SecretQuestion = secretQuestion, HashPassword = hashPassword, Salt = salt,
                                      Verified = false };
                _dbContext.Add(user);
                _dbContext.SaveChanges();

                // Commit transaction if all commands succeed, transaction will auto-rollback
                // when disposed if either commands fails
                transaction.Commit();
                return user;
            }
        }

        public User GetUserByEmailPassword(string email, string password)
        {
            var user = _dbContext.Users
                .Single(b => b.Email == email);
            var hashPassword = Hash.HashString(password, user.Salt);
            if (user.HashPassword == hashPassword)
                return user;
            else
                return null;
        }

        public List<User> GetUserList()
        {
            return _dbContext.Users.ToList();
        }

        public User GetUser(int Id)
        {
            return _dbContext.Users
                .Single(b => b.UserId == Id);
        }

        public User GetUserByEmail(string email)
        {
            return _dbContext.Users
                .Single(b => b.Email == email);
        }

        public User UpdateUser(User user)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                _dbContext.Update(user);
                _dbContext.SaveChanges();

                transaction.Commit();
                return user;
            }
        }

        public bool DeleteUser(User user)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                _dbContext.Remove(user);
                _dbContext.SaveChanges();

                transaction.Commit();
                return true;
            }
        }
    }
}
