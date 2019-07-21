using NoteApp.BaseGenerator;
using NoteApp.Models;
using System.IO;

namespace GeneratorLibrary.Formats
{
    public class TxtGenerator : Generator
    {
        public TxtGenerator()
        {
            Name = "txt";
        }
        public override string Generate(Resume resume, string fileUri, string fileName)
        { 
            var newDir = "Text\\";
            DirectoryInfo dirInfo = new DirectoryInfo(fileUri);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(newDir);
            string[] path = { fileUri, newDir, fileName };
            string file = Path.Combine(path);
            using (StreamWriter sw = new StreamWriter(file, true))
            {
                sw.WriteLine($"ФИО: {resume.FIO}");

                sw.WriteLine($"Дата рождения: {resume.Birthday}");

                sw.WriteLine($"Прошлые места работы: {resume.PastPlaces}");

                sw.Close();
            }
            return file;
        }
    }
}