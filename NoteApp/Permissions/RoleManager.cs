using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using NoteApp.Models;

namespace NoteApp.Permission
{
    public class RoleManager : RoleManager<Role, long>
    {
        public RoleManager(IRoleStore<Role, long> store) : base(store)
        {
            RoleValidator = new RoleValidator<Role, long>(this);
        }
    }
}