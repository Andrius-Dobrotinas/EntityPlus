using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecordLabel.TheContext
{
    public abstract class Content
    {
        [Key]
        public int Id { get; set; }

        public virtual IList<Metadata> Metadata
        {
            get
            {
                return metadata ?? (metadata = new List<Metadata>());
            }
            set
            {
                metadata = value;
            }
        }
        private IList<Metadata> metadata { get; set; }
        
        public virtual IList<Reference> References
        {
            get
            {
                return reference ?? (reference = new List<Reference>());
            }
            set
            {
                reference = value;
            }
        }
        private IList<Reference> reference { get; set; }
    }
}
