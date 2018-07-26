using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using NoteApp.Models;

namespace NoteApp.Auth
{
    public class UserManager : UserManager<User, long>
    {
        public UserManager(IUserStore<User, long> store)
            : base(store)
        {
            UserValidator = new UserValidator<User, long>(this);
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4
            };
        }
    }
}