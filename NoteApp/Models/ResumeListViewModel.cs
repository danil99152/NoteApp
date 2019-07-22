using NoteApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteApp.Models
{
    public class ResumeListViewModel
    {
        public ICollection<Resume> Resumes { get; set; }

        public ResumeListViewModel()
        {
            Resumes = new List<Resume>();
        }
    }
}