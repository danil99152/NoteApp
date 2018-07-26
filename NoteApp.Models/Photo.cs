using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Models
{
    public class Photo
    {
        public virtual long Id { get; set; }

        public virtual User User { get; set; }

        public virtual string Content { get; set; }
    }
}
