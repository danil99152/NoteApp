using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using NHibernate;
using NHibernate.Criterion;

namespace NoteApp.Models.Repositories
{
    [Repository]
    public class ResumeRepository : Repository<Resume>
    {
        public ResumeRepository(ISession session) : base(session)
        {
        }

        public IList<Resume> GetAllByUser(User user, FetchOptions options = null)
        {
            var crit = session.CreateCriteria<Resume>().Add(Restrictions.Eq("FIO", user));
            if (options != null)
            {
                SetFetchOptions(crit, options);
            }
            return crit.List<Resume>();
        }
    }
}
