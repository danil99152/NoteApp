﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Models
{
    public class BinaryFile
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Path { get; set; }

        public virtual Note Note { get; set; }

        public virtual string ContentType { get; set; }

        public BinaryFile()
        {

        }

        public BinaryFile(string name, string path) : this()
        {
            Name = name;
            Path = path;
        }
    }
}
