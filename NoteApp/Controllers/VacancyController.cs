using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoteApp.Files;
using NoteApp.Models;
using NoteApp.Models.Models;
using NoteApp.Models.Repositories;

namespace NoteApp.Controllers
{
    [Authorize]
    public class VacancyController : BaseController
    {
        private readonly VacancyRepository vacancyRepository;
        public VacancyController(UserRepository userRepository, IFileProvider[] fileProviders, VacancyRepository vacancyRepository)
            : base(userRepository, fileProviders)
        {
            this.vacancyRepository = vacancyRepository;
        }
        [Authorize(Roles = "Employer")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        public ActionResult Create(VacancyEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.GetCurrentUser(User);
                var vacancy = new Vacancy
                {
                    Name = model.Name,
                    Author = user,
                    Description = model.Description,
                    Time = model.Time,
                    Company = model.Company,
                    Requirments = model.Requirments,
                    Salary = model.Salary
                };
                vacancyRepository.Save(vacancy);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Employer")]
        public ActionResult Delete(long vacancyId)
        {
            var vacancy = vacancyRepository.Load(vacancyId);
            vacancyRepository.Delete(vacancy);
            return RedirectToAction("Index");
        }

        public ActionResult Details(long vacancyId)
        {
            var vacancy = vacancyRepository.Load(vacancyId);
            return PartialView("Details", vacancy);
        }

        public ActionResult Index(FetchOptions options)
        {
            var vacancies = vacancyRepository.GetAllVacancies(options);
            var model = new VacancyListViewModel
            {
                Vacancies = vacancies
            };
            return View(model);
        }
    }
}