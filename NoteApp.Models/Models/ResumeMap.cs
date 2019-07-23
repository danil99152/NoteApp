using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace NoteApp.Models.Models
{
    public class ResumeMap : ClassMap<Resume>
    {
        public ResumeMap()
        {
            Id(r => r.Id).GeneratedBy.Identity();
            Map(r => r.FIO);
            Map(r => r.Birthday);
            Map(r => r.PastPlaces).Length(int.MaxValue);
            Map(r => r.Requirments);
            HasOne(r => r.Photo).Cascade.All().Constrained();
            References(r => r.Author).Column("User_Id");
        }
    }
}
