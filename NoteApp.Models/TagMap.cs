using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace NoteApp.Models
{
    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            Id(t => t.Id).GeneratedBy.Identity();
            Map(t => t.Name).Length(30).Unique();

        }
    }
}
