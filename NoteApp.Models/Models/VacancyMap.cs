using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Models.Models
{
    public class VacancyMap : ClassMap<Vacancy>
    {
        public VacancyMap()
        {
            Id(v => v.Id).GeneratedBy.Identity();
            Map(v => v.Name).Length(30);
            References(v => v.Author).Column("User_Id");
            Map(v => v.Description).Length(4001);
            Map(v => v.Time);
            Map(v => v.Company);
            //  References(v => v.Requirments).Column("Requirment_id");
            Map(v => v.Requirments);
            Map(v => v.Salary);
        }
    }
}
