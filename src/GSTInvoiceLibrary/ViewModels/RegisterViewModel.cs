using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GSTInvoiceData.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name Required")]
        [Display(Name = "First Name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Required")]
        [Display(Name = "Last Name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Id")]
        [Remote("IsEmailExist", "User", HttpMethod = "POST", ErrorMessage = "Account already exists, Please <a class='signin' href='#'>Sign In</a>")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password Should contain min 8 character's")]
        public string Password { get; set; }
    }
}
