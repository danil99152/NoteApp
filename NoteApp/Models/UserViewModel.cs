using NoteApp.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models
{
    public class UserViewModel : EntityViewModel<User>
    {
        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        [Display(Name = "Подтвердите пароль")]
        public string ConfirmPassword { get; set; }
    }
}