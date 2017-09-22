using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace GSTInvoiceData.Models
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Salutation { get; set; }

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

        [Display(Name = "Mobile No.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password Should contain min 8 character's")]
        public string Password { get; set; }

        //[NotMapped]
        //[DataType(DataType.Password)]
        //[System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Confirm Password does not match")]
        //[Display(Name = "Confirm Password")]
        //public string ConfirmPassword { get; set; }

        public string Gender { get; set; }

        public string Language { get; set; }

        public string Country { get; set; }

        public int UserType { get; set; }

        public bool IsEmailVerified { get; set; }

        public DateTime RequestDateTime { get; set; }

        public string RequestTokenNo { get; set; }
    }

    public class UserForGotPassword
    {
        [Required(ErrorMessage = "Please enter your email address or mobile no.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Id")]
        [Remote("EmailNotExist", "User", HttpMethod = "POST", ErrorMessage = "Account does not exists, Please <a class='register' href='#'>Register</a>.")]
        public string EmailId { get; set; }
    }

    public class UserLogin : UserForGotPassword
    {
        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password Should contain min 8 character's")]
        public string Password { get; set; }
    }

    public enum UserType
    {
        Basic,
        Professional,
        Enterprise
    }
}
