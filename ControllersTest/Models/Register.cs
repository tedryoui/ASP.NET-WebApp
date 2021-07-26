using System.ComponentModel.DataAnnotations;
 
namespace ControllersTest.Models
{
    public class Register
    {
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
 
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
 
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}