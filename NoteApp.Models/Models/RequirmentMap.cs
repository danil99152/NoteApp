using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace NoteApp.Models.Models
{
    public class RequirmentMap : ClassMap<Requirment>
    {
        public RequirmentMap()
        {
            Id(t => t.Id).GeneratedBy.Identity();
            Map(t => t.Name).Length(30).Unique();

        }
    }
}
