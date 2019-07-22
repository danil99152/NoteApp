using NoteApp.Models;
using NoteApp.Models.Models;

namespace NoteApp.BaseGenerator
{
    public class Generator
    {
        public string Name { get; set; }
        /// <summary>
        /// Abstract method to realise methods for generating formats
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public virtual string Generate(Resume person, string fileUri, string fileName)
        {
            return null;
        }
    }
}