using NoteApp.Models.Models;
using System.IO;

namespace NoteApp.Files
{
    public interface IFileProvider
    {
        string Name { get; }

        void Save(BinaryFile file);

        FileStream Load(BinaryFile file);
    }
}