using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecordLabel.TheContext
{
    public class Metadata
    {
        [Key]
        public int Id { get; set; }

        public MetadataType Type { get; set; }

        public string Text { get; set; }

        public virtual IList<Content> Targets
        {
            get
            {
                return targets ?? (targets = new List<Content>());
            }
            set
            {
                targets = value;
            }
        }
        private IList<Content> targets { get; set; }
    }
}
