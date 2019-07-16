using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDWebAPI.Models.Data
{
    public class DVD
    {
        public int dvdID { get; set; }
        public string title { get; set; }
        public int releaseYear { get; set; }
        public int directorID { get; set; }
        public int ratingID { get; set; }
        public string notes { get; set; }

        public string directorFirstName { get; set; }
        public string directorMiddleName { get; set; }
        public string directorLastName { get; set; }

        public string rating { get; set; }

        public virtual List<Ratings> ratings { get; set; }
        public virtual List<Director> director { get; set; }
    }
}