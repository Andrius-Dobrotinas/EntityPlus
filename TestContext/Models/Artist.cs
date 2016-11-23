using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecordLabel.TheContext
{
    public class Artist : Content
    {
        [Required]
        public string Name { get; set; }

        public string Text { get; set; }
    }
}
