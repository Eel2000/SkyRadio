using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyRadio.Application.DTOs.Identity
{
    public class RegistrationRequest
    {
        [Required(ErrorMessage ="Please specify the username")]
        [MinLength(5, ErrorMessage ="Must have at least 6 characters")]
        [MaxLength(255, ErrorMessage ="Please ensure the chars are not out of required length")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage ="The email input must be a real email.")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please specify the email.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password input is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [Required(ErrorMessage ="Please enter the confiramtion password")]
        public string ConfirmPassword { get; set; }
    }
}
