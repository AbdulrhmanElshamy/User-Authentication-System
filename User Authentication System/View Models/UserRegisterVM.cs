using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using User_Authentication_System.constnet;

namespace User_Authentication_System.View_Models
{
    public class UserRegisterVM
    {
        [Required(ErrorMessage = Errors.EmailRequired)]
        [Remote("AllowUser", "Account", ErrorMessage = Errors.UserAlredyExist)]
        public string User { get; set; }

        [Required(ErrorMessage = Errors.EmailRequired)]
        [Remote("AllowEmail", "Account", ErrorMessage = Errors.EmailAlredyExist)]
        public string Email { get; set; }

        [Required(ErrorMessage = Errors.PasswordRequired)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = Errors.ConfirmRequired)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = Errors.PasswordNotMatch)]
        public string ConfirmPassword { get; set; }
    }
}
