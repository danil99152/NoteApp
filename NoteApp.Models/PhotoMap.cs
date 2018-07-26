using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace NoteApp.Models
{
    public class PhotoMap : ClassMap<Photo>
    {
        public PhotoMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            References(p => p.User);
            Map(p => p.Content);
        }
    }
}
