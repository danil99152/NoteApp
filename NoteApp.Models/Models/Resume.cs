﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Models
{
    public class Resume
    {
        public virtual long Id { get; set; }

        [Display(Name = "Автор")]
        public virtual User FIO { get; set; }

        [Display(Name = "Дата рождения")]
        public virtual DateTime Birthday { get; set; }

        [Display(Name = "Прошлые места работы")]
        public virtual string PastPlaces { get; set; }

        public virtual BinaryFile Photo { get; set; }

        [Display(Name = "Навыки")]
        public virtual string Requirments { get; set; }

        [Display(Name = "В каком формате вы хотите файл")]
        public virtual string Type { get; set; }
    }
}