using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GSTInvoiceData.ViewModels
{
    public class LoginViewModel : ForGotPasswordViewModel
    {
        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password Should contain min 8 character's")]
        public string Password { get; set; }
    }

    public class ForGotPasswordViewModel
    {
        [Required(ErrorMessage = "Please enter your email address.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Id")]
        [Remote("EmailNotExist", "User", HttpMethod = "POST", ErrorMessage = "Account does not exists, Please <a class='register' href='#'>Register</a>.")]
        public string EmailId { get; set; }
    }
}
