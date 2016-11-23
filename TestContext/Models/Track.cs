using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecordLabel.TheContext
{
    public class Track
    {
        [Key]
        public int Id { get; set; }

        public int ReleaseId { get; set; }
        public virtual Release Release { get; set; }

        [Required]
        public string Title { get; set; }

        public string Reference { get; set; }
    }
}
