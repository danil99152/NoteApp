﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace NoteApp.Models
{
    public class NoteMap : ClassMap<Note>
    {
        public NoteMap()
        {
            Id(n => n.Id).GeneratedBy.Identity();
            Map(n => n.Title).Length(30);
            Map(n => n.Text).Length(int.MaxValue);
            Map(n => n.ChangeDate);
            Map(n => n.CreationDate);
            Map(n => n.Tags);
            References(n => n.Author).Column("User_id");
            HasMany(n => n.Files).KeyColumn("Note_id").Cascade.All();
        }
    }
}
