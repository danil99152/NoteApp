using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Models.Models
{
    public class Vacancy
    {
        public virtual long Id { get; set; }

        [Display(Name = "Название вакансии")]
        public virtual string Name { get; set; }

        public virtual User Author { get; set; }

        [Display(Name = "Описание вакансии")]
        public virtual string Description { get; set; }

        [Display(Name = "Срок заключения сделки")]
        public virtual int Time { get; set; }

        [Display(Name = "Компания")]
        public virtual string Company { get; set; }

        [Display(Name = "Требования к кандидату")]
        public virtual string Requirments { get; set; }

        [Display(Name = "Зарплата")]
        public virtual int Salary { get; set; }

        /*
        public Vacancy()
        {
            Requirments = new List<Requirment>();
        }
        */

    }
}
