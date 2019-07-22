using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Event;
using NoteApp.Models.Models;
using NoteApp.Models.Repositories;

namespace NoteApp.Models.Listeners
{
    [Listener]
    public class PreInsertResumeEventListener : IPreInsertEventListener
    {
        public bool OnPreInsert(PreInsertEvent @event)
        {
            return SetCreationProps(@event);
        }

        public Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
        {
            return Task.FromResult(SetCreationProps(@event));
        }

        private bool SetCreationProps(PreInsertEvent @event)
        {
            if (@event.Entity is Resume candidate)
            {
                return true;
            }
            return false;
        }
    }
}
