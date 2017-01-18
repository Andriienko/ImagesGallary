using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SocialImagesGallary.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="User Name required")]
        [MaxLength(15)]
        [Display(Name="User Name *")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        [Display(Name="Email *")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name required")]
        [MaxLength(30)]
        [Display(Name = "Name *")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adress required")]
        [MaxLength(40)]
        [Display(Name = "Adress *")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password *")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password required")]
        [Display(Name = "Confirm Password *")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Passwords not match")]
        public string ConfirmPassword { get; set; }



    }
}