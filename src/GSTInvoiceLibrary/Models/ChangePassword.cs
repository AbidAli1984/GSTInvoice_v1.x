using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTInvoiceData.Models
{
   public class ResetPassword
    {
        public string RequestToken { get; set; }

        [Required(ErrorMessage ="Please enter new password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Password not matched")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePassword:ResetPassword
    {
        [Required(ErrorMessage ="Please enter Old password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}
