using System.ComponentModel.DataAnnotations;

namespace TrashPandaNet.Logic.Models
{
    public class UserEditViewModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Ник")]
        public string UserName => Email;

        [Required]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Изображение")]
        public string Image { get; set; }

        [Display(Name = "Статус")]
        public string Status { get; set; }

        [Display(Name = "Обо мне")]
        public string About { get; set; }
    }
}
