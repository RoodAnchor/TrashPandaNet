﻿using System.ComponentModel.DataAnnotations;

namespace TrashPandaNet.Logic.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Это поле необходимо заполнить")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Это поле необходимо заполнить")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Это поле необходимо заполнить")]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Это поле необходимо заполнить")]
        [Display(Name = "Email")]
        public string EmailReg { get; set; }

        [Required(ErrorMessage = "Это поле необходимо заполнить")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        public string PasswordReg { get; set; }

        [Required(ErrorMessage = "Это поле необходимо заполнить")]
        [Compare("PasswordReg", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Это поле необходимо заполнить")]
        [Display(Name = "Никнейм")]
        public string UserName => EmailReg;

        [Required(ErrorMessage = "Это поле необходимо заполнить")]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; } = DateTime.Now.AddYears(-50);
    }
}
