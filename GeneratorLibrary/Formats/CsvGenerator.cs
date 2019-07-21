using NoteApp.BaseGenerator;
using NoteApp.Models;
using System.IO;

namespace GeneratorLibrary.Formats
{
    public class CsvGenerator : Generator
    {
        public CsvGenerator()
        {
            Name = "csv";
        }
        public override string Generate(Resume resume, string fileUri, string fileName)
        {
            var newDir = "CSV\\";
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
                var text = $"ФИО: {resume.FIO}" + $"Дата рождения: {resume.Birthday}" + $"Прошлые места работы: {resume.PastPlaces}";
                var csv = text.Split(';');
                sw.WriteLine(csv);
                sw.Close();
            }
            return file;
        }
    }
}