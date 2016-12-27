using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpSQlite.Model
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string HashPassword { get; set; }
        [Required]
        public string Salt { get; set; }

        public string SecretQuestion { get; set; }

        public bool Verified { get; set; }
        public string VerificationCode { get; set; }
        public string ResetCode { get; set; }

        [InverseProperty("Author")]
        public List<Post> AuthoredPosts { get; set; }

        [InverseProperty("Contributor")]
        public List<Post> ContributedToPosts { get; set; }
    }
}
