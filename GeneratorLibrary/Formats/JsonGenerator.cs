using NoteApp.BaseGenerator;
using NoteApp.Models;
using System.IO;
using System.Runtime.Serialization.Json;

namespace GeneratorLibrary.Formats
{
    public class JsonGenerator : Generator
    {
        public JsonGenerator()
        {
            Name = "json";
        }
        public override string Generate(Resume resume, string fileUri, string fileName)
        {
            var newDir = "Json\\";
            DirectoryInfo dirInfo = new DirectoryInfo(fileUri);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(newDir);
            string[] path = { fileUri, newDir, fileName };
            string file = Path.Combine(path);
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Resume));
            using (FileStream sw = new FileStream(file, FileMode.Create))
            {
                json.WriteObject(sw, $"ФИО: {resume.FIO}");
                json.WriteObject(sw, $"Дата рождения: {resume.Birthday}");
                json.WriteObject(sw, $"Прошлые места работы: {resume.PastPlaces}");
                sw.Close();
            }
            return file;
        }
    }
}