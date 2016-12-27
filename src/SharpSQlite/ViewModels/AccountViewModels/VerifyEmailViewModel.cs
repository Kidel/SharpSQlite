using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharpSQlite.AccountViewModels
{
    public class VerifyEmailViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "code")]
        public string Code { get; set; }
    }
}
