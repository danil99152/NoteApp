using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace NoteApp.Models
{
    public class BinaryFileMap : ClassMap<BinaryFile>
    {
        public BinaryFileMap()
        {
            Id(f => f.Id).GeneratedBy.Identity();
            Map(f => f.Name).Length(64);
            Map(f => f.Path).Length(255);
            References(f => f.Note).Column("Note_id");
        }
    }
}
