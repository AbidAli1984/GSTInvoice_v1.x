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

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailId { get; set; }

        public string ContactNumber { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
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

    public enum UserType
    {
        Basic,
        Professional,
        Enterprise
    }
}
