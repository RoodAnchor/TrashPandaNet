using System.ComponentModel.DataAnnotations;

namespace TrashPandaNet.Logic.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
