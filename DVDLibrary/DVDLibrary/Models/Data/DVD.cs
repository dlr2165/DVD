using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace DVDLibrary.Models.Data
{
    public class DVD
    {
        public int DvdId { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Notes { get; set; }
    }
}