using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace NoteApp.Models
{
    public class ResumeMap : ClassMap<Resume>
    {
        public ResumeMap()
        {
            Id(r => r.Id).GeneratedBy.Assigned().Not.Nullable();
            References(r => r.FIO).Column("User_id");
            Map(r => r.Birthday);
            Map(r => r.PastPlaces).Length(int.MaxValue);
            Map(r => r.Requirments);
            References(r => r.Photo).Column("BinaryFile_id");
        }
    }
}
