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
    public class PreUpdateResumeEventListener : IPreUpdateEventListener
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
            //if (@event.Entity is Resume candidate)
            //{
            //    return true;
            //}
            return false;
        }
    }
}
