using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Event;
using NoteApp.Models.Repositories;

namespace NoteApp.Models.Listeners
{
    [Listener]
    public class PreUpdateNoteEventListener : IPreUpdateEventListener
    {
        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            return SetProps(@event);
        }

        public Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
        {
            return Task.FromResult(SetProps(@event));
        }

        private bool SetProps(PreUpdateEvent @event)
        {
            if (@event.Entity is Note note)
            {
                note.ChangeDate = DateTime.Now;
            }
            return false;
        }
    }
}
