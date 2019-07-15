﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoteApp.Files;
using NoteApp.Models;
using NoteApp.Models.Repositories;

namespace NoteApp.Controllers
{
    [Authorize]
    public class NoteController : BaseController
    {
        private readonly ResumeRepository noteRepository;

        public NoteController(ResumeRepository noteRepository, IFileProvider[] fileProviders ,UserRepository userRepository) : 
            base(userRepository, fileProviders)
        {
            this.noteRepository = noteRepository;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NoteEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var files = new List<BinaryFile>();
                string serverPath = Server.MapPath($"~/Uploaded Files/{ User.Identity.Name }/");
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                foreach (var file in model.Files)
                {
                    if (file != null)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string filePath = Path.Combine(serverPath, fileName);
                        file.SaveAs(filePath);
                        files.Add(new BinaryFile(fileName, filePath));
                    }
                }
                var user = userRepository.GetCurrentUser(User);
                var note = new Resume
                {
                    Title = model.Title,
                    PastPlaces = model.Text,
                    Tags = model.Tags,
                    FIO = user,
                    Files = files
                };
                noteRepository.Save(note);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public FileResult Download(string filePath, string fileName)
        {
            return File(filePath, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult Delete(long noteId)
        {
            var note = noteRepository.Load(noteId);
            noteRepository.Delete(note);
            return RedirectToAction("Index");
        }

        public ActionResult Details(long noteId)
        {
            var note = noteRepository.Load(noteId);
            var user = userRepository.GetCurrentUser(User);
            if (user.Equals(note.FIO))
            {
                return PartialView("Details", note);
            }
            return HttpNotFound();
        }

        public ActionResult Index(FetchOptions options)
        {
            var user = userRepository.GetCurrentUser(User);
            var notes = noteRepository.GetAllByUser(user, options);
            var model = new NoteListViewModel
            {
                Notes = notes
            };
            return View(model);
        }
    }
}