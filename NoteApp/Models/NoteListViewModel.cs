using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteApp.Models
{
    public class NoteListViewModel
    {
        public ICollection<Note> Notes { get; set; }

        public NoteListViewModel()
        {
            Notes = new List<Note>();
        }
    }
}