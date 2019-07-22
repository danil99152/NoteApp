using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace NoteApp.Models.Models
{
    public class User : IUser<long>
    {
        public virtual long Id { get; set; }

        [Display(Name = "ФИО")]
        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual bool IsEnabled { get; set; } = true;

        public User()
        {
            Roles = new List<Role>();
        }

        public User(string userName) : this()
        {
            UserName = userName;
        }
    }
}
