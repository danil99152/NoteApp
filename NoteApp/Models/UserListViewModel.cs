using NoteApp.Models.Models;
using System.Collections.Generic;

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