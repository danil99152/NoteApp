using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Models
{
    public class Note
    {
        public virtual long Id { get; set; }

        [Display(Name = "Заголовок")]
        public virtual string Title { get; set; }

        [Display(Name = "Текст")]
        public virtual string Text { get; set; }

        [Display(Name = "Дата создания заметки")]
        public virtual DateTime CreationDate { get; set; }

        [Display(Name = "Дата изменения заметки")]
        public virtual DateTime ChangeDate { get; set; }

        [Display(Name = "Автор")]
        public virtual User Author { get; set; }

        public virtual ICollection<BinaryFile> Files { get; set; }

        [Display(Name = "Тэги")]
        public virtual string Tags { get; set; }

        public Note()
        {
            Files = new List<BinaryFile>();
        }
    }
}
