using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace NoteApp.Models.Models
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(u => u.Id).GeneratedBy.Identity();
            Map(u => u.UserName).Length(30);
            Map(u => u.Password).Column("PasswordHash");
            HasManyToMany(u => u.Roles)
                .ParentKeyColumn("User_id")
                .ChildKeyColumn("Role_id");
            Map(u => u.IsEnabled);
        }
    }
}
