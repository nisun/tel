using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Telehire.Web.Models
{
    public class AdminRegisterUserModel
    {
        public bool IsEditing { get; set; }

        public AdminRegisterUserModel()
        {
            this.SystemRoles = new List<SelectListItem>();

        }

        public long pId { get; set; }

        public Guid UserId { get; set; }

        [DisplayName("User Role:")]
        public string UserRole { get; set; }

        public List<SelectListItem> SystemRoles { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [DisplayName("New Password:")]
        //[ValidatePasswordLength(ErrorMessage = "Password lenght cannot be less than 8 characters. It must also be alphanumeric.")]
        [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,})$", ErrorMessage = "Enter a valid Password")]
        public string NewPassword { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        //[ValidatePasswordLength(ErrorMessage = "Password lenght cannot be less than 8 characters. It must also be alphanumeric.")]
        [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,})$", ErrorMessage = "Enter a valid Password")]
        [DisplayName("Confirm Password:")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Active:")]
        public bool IsApproved { get; set; }

        //[DisplayName("UnLock User:")]
        //public bool UnLockUser { get; set; }

        [DisplayName("First Name:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string FirstName { get; set; }

        [DisplayName("Last Name:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string LastName { get; set; }


        [DisplayName("Email Address:")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Enter a valid Email Address")]

        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string Email { get; set; }

        [DisplayName("Phone Number:")]
        [StringLength(11, ErrorMessage = "Phone number not valid")]
        [RegularExpression(@"^\s*\+?\s*([0-9][\s-]*){11,}$", ErrorMessage = "Phone number not valid")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string PhoneNumber { get; set; }



    }
}