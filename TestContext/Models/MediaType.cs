using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecordLabel.TheContext
{
    public class MediaType
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }
    }
}
