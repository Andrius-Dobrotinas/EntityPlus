using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecordLabel.TheContext
{
    public class Reference
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Target { get; set; }

        [Required]
        public ReferenceType Type { get; set; }

        public int Order { get; set; }

        public Content Owner { get; set; }
    }
}
