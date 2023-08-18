using System.ComponentModel.DataAnnotations;
using User_Authentication_System.constnet;

namespace User_Authentication_System.View_Models
{
    public class UserLoginVM
    {
        [Required(ErrorMessage = Errors.EmailRequired)]
        public string Email { get; set; }

        [Required(ErrorMessage = Errors.PasswordRequired)]
        public string Password { get; set; }
    }
}
