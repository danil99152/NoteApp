﻿using System;
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
    public class PreInsertNoteEventListener : IPreInsertEventListener
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
            if (@event.Entity is Note note)
            {
                note.CreationDate = DateTime.Now;
                note.ChangeDate = DateTime.Now;
            }
            else if (@event.Entity is User user)
            {
                user.RegistrationDate = DateTime.Now;
            }
            return false;
        }
    }
}
