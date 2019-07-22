using Microsoft.AspNet.Identity;
using NoteApp.Models.Models;

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