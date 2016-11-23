using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RecordLabel.TheContext
{
    public class DropCreateAndSeedInitializer<T> : DropCreateDatabaseAlways<T> where T : ReleaseContext
    {
        protected override void Seed(T db)
        {
            // MEDIA TYPES
            var media_LP = new MediaType { Text = "LP" };
            var media_CD = new MediaType { Text = "CD" };
            var media_FLAC = new MediaType { Text = "FLAC" };


            // ARTISTS
            var artist1 = new Artist
            {
                Name = "Iggy & The Stooges",
                Text = "Iggy with Asheton bros and the new guy on guitar"
            };
            db.Artists.Add(artist1);

            var artist2 = new Artist
            {
                Name = "The Stooges",
                Text = "Iggy, Asheton bros and Dave"
            };
            db.Artists.Add(artist2);


            // RELEASES
            var release1 = new Release
            {
                Artist = artist1,
                Title = "Raw Power",
                CatalogueNumber = "KC3200",
                Date = 1973,
                Media = media_LP,
                Text = "This is awesome!"
            };
            db.Releases.Add(release1);

            var release2 = new Release
            {
                Artist = artist2,
                Title = "The Stooges",
                CatalogueNumber = "EK74000",
                Date = 1969,
                Media = media_LP,
                Text = "Their first record"
            };
            db.Releases.Add(release2);

            var release3 = new Release
            {
                Artist = artist2,
                Title = "Fun House",
                CatalogueNumber = "EK74007",
                Date = 1971,
                Media = media_LP,
                Text = "They had no idea this would be the best rock and roll record ever!"
            };
            db.Releases.Add(release3);

            db.SaveChanges();


            // METADATA
            var metadata1 = new Metadata
            {
                Type = MetadataType.Genre,
                Text = "Hard Rock"
            };
            var metadata2 = new Metadata
            {
                Type = MetadataType.Attribute,
                Text = "Classic Rock"
            };
            var metadata3 = new Metadata
            {
                Type = MetadataType.Attribute,
                Text = "Heavy Metal"
            };

            metadata1.Targets.Add(artist1);
            metadata1.Targets.Add(release1);
            metadata1.Targets.Add(release2);
            metadata2.Targets.Add(release1);
            metadata2.Targets.Add(release3);

            db.Metadata.Add(metadata1);
            db.Metadata.Add(metadata2);
            db.Metadata.Add(metadata3);

            db.SaveChanges();


            // TRACKS
            var track1 = new Track { Title = "Search And Destroy" };
            track1.Reference = "http://www.google.com";
            release1.Tracks.Add(track1);

            var track2 = new Track { Title = "Blank" };
            release1.Tracks.Add(track2);
            db.SaveChanges();

            var track3 = new Track { Title = "Gotcha!" };
            track3.Reference = "http://www.youtube.com";

            release1.Tracks.Add(track3);

            db.SaveChanges();


            // REFERENCES
            var reference1 = new Reference { Target = "http://www.metallica.com", Order = 20, Type = ReferenceType.File };
            release1.References.Add(reference1);

            db.SaveChanges();

            base.Seed(db);
        }
    }
}
