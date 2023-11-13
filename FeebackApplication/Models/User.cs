using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FeebackApplication.Models
{
    [Table (("User"),Schema ="dbo") ]
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is Required")]

        public string LastName { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [DisplayName ("Confirm Password ")]
        [Compare("Password")]
       
        public string ConfirmPassword { get; set; }
    }
}