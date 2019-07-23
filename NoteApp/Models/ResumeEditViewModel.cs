using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoteApp.Models
{
    public class ResumeEditViewModel
    {
        [Display(Name = "Ваше полное Имя")]
        [Required(ErrorMessage = "Необходимо ввести ваше полное Имя")]
        public virtual string FIO { get; set; }

        [Display(Name = "Ваша дата рождения")]
        [Required(ErrorMessage = "Необходимо ввести вашу дату рождения")]
        public virtual DateTime Birthday { get; set; }

        [AllowHtml]
        [Display(Name = "Ваши предыдущие места работы")]
        [DataType(DataType.MultilineText)]
        [StringLength(int.MaxValue, ErrorMessage = "Содержимое предыдущих мест работ слишком большое")]
        public string PastPlaces { get; set; }

        [Display(Name = "Ваши навыки")]
        [StringLength(255, ErrorMessage = "Содержимое навыков слишком большое")]
        public string Requirments { get; set; }

    }
}