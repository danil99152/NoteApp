﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteApp.Models
{
    public abstract class EntityViewModel<T>
    {
        public T Entity { get; set; }
    }
}