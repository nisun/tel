using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Telehire.Web.Models
{
    public class LoginModel
    {
        [DisplayName("Email Address: ")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter a valid Email Address")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Enter a valid Email Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string Password { get; set; }
    }
}