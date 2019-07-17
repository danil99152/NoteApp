using NoteApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteApp.Models
{
    public class VacancyListViewModel
    {
        public ICollection<Vacancy> Vacancies { get; set; }

        public VacancyListViewModel()
        {
            Vacancies = new List<Vacancy>();
        }
    }
}