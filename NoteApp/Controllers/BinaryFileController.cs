using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoteApp.Files;
using NoteApp.Models.Repositories;

namespace NoteApp.Controllers
{
    [Authorize]
    public class BinaryFileController : BaseController
    {
        private BinaryFileRepository binaryFileRepository;

        public BinaryFileController(UserRepository userRepository, IFileProvider[] fileProviders, BinaryFileRepository binaryFileRepository)
            : base(userRepository, fileProviders)
        {
            this.binaryFileRepository = binaryFileRepository;
        }

        public ActionResult Download(long id)
        {
            var binaryFile = binaryFileRepository.Load(id);
            var stream = GetFileProvider().Load(binaryFile);
            return File(stream, binaryFile.ContentType);
        }
    }
}