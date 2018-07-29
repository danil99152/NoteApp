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

        void Save(BinaryFile file);

        FileStream Load(BinaryFile file);
    }
}