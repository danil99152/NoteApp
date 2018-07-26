using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteApp.Models
{
    public class UserListViewModel
    {
        public ICollection<User> Users { get; set; }

        public UserListViewModel()
        {
            Users = new List<User>();
        }
    }
}