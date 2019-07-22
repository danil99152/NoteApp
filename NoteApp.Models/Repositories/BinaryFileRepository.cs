using NHibernate;
using NoteApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Models.Repositories
{
    [Repository]
    public class BinaryFileRepository : Repository<BinaryFile>
    {
        public BinaryFileRepository(ISession session) : base(session)
        {
        }
    }
}