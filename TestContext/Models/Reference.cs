using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecordLabel.TheContext
{
    public class Reference : ReferenceBase
    {
        /*[Key]
        public int Id { get; set; }*/

        public Content Owner { get; set; }

        //private IList<Content> owners;
    }
}
