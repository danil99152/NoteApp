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
            Id(n => n.Id);
            References(n => n.FIO).Column("User_id");
            Map(n => n.Birthday);
            Map(n => n.PastPlaces).Length(int.MaxValue);
            Map(n => n.Requirments);
            References(n => n.Photo).Column("BinaryFile_id");
        }
    }
}
