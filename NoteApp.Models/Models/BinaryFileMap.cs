using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace NoteApp.Models.Models
{
    public class BinaryFileMap : ClassMap<BinaryFile>
    {
        public BinaryFileMap()
        {
            Id(f => f.Id).GeneratedBy.Identity();
            Map(f => f.Name).Length(100);
            Map(f => f.Path).Length(255);
            Map(f => f.ContentType);
            HasOne(f => f.Candidate);
        }
    }
}