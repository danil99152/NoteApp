using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using NHibernate;
using NHibernate.Criterion;
using NoteApp.Models.Models;

namespace NoteApp.Models.Repositories
{
    [Repository]
    public class ResumeRepository : Repository<Resume>
    {
        public ResumeRepository(ISession session) : base(session)
        {
        }

        public IList<Resume> GetAllResumes(FetchOptions options = null)
        {
            var crit = session.CreateCriteria<Resume>();
            if (options != null)
            {
                SetFetchOptions(crit, options);
            }
            return crit.List<Resume>();
        }
    }
}
