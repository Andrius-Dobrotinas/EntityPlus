using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordLabel.TheContext
{
    public class Release : Content
    {
        [Required]
        public string Title { get; set; }

        public string Text { get; set; }

        [ForeignKey("Artist"), Required]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        
        public short Date { get; set; }

        public short Length { get; set; }

        [Required]
        public string CatalogueNumber { get; set; }

        [ForeignKey("Media"), Required]
        public int MediaId { get; set; }
        public MediaType Media { get; set; }

        [Required]
        public PrintStatus PrintStatus { get; set; } = PrintStatus.InPrint;

        public virtual IList<Track> Tracks
        {
            get
            {
                return tracks ?? (tracks = new List<Track>());
            }
            set
            {
                tracks = value;
            }
        }
        private IList<Track> tracks { get; set; }
    }
}
