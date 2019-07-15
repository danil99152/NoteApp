using NoteApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NoteApp.Files
{
    public interface IFileProvider
    {
        string Name { get; }

        void Save(Resume file);

        FileStream Load(Resume file);
    }
}