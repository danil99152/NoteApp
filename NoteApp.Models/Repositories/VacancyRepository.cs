﻿using NHibernate;
using NHibernate.Criterion;
using NoteApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Models.Repositories
{
    [Repository]
    public class VacancyRepository : Repository<Vacancy>
    {
        public VacancyRepository(ISession session) : base(session)
        {
        }

        public IList<Vacancy> GetAllByUser(User user, FetchOptions options = null)
        {
            var crit = session.CreateCriteria<Vacancy>().Add(Restrictions.Eq("FIO", user));
            if (options != null)
            {
                SetFetchOptions(crit, options);
            }
            return crit.List<Vacancy>();
        }
    }
}