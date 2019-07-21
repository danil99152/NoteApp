using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoteApp.Models
{
    public class VacancyEditViewModel
    {
        [Display(Name = "Название вакансии")]
        [Required(ErrorMessage = "Необходимо ввести название вашей вакансии")]
        public virtual string Name { get; set; }

        [Display(Name = "Описание вакансии")]
        [Required(ErrorMessage = "Необходимо ввести описание вашей вакансии")]
        public virtual string Description { get; set; }

        [Display(Name = "Срок заключения сделки")]
        [Required(ErrorMessage = "Срок заключения сделки!")]
        public virtual int Time { get; set; }

        [Display(Name = "Компания")]
        [Required(ErrorMessage = "А компания?")]
        public virtual string Company { get; set; }

        [Display(Name = "Требования к кандидату")]
        [Required(ErrorMessage = "Потеряли")]
        public virtual string Requirments { get; set; }

        [Display(Name = "Зарплата")]
        [Required(ErrorMessage = "За бесплатно никто работать не будет!")]
        public virtual int Salary { get; set; }

    }
}