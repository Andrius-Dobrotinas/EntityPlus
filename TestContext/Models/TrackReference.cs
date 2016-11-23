using System;
using System.Collections.Generic;

namespace RecordLabel.TheContext
{
    public class TrackReference : ReferenceBase
    {
        //[Key, ForeignKey("Track")]
        //public int Id { get; set; } // TODO: move this to the base class
        // TODO: add public int TrackId (foreign key)
        public virtual Track Track { get; set; }
    }
}
