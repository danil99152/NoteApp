﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace NoteApp.Models
{
    public class Role : IRole<long>
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }

        public Role(string roleName) : this()
        {
            Name = roleName;
        }
    }
}
