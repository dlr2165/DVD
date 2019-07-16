using DVDWebAPI.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDWebAPI.Data
{
    public interface IDVDRepository
    {
        List<DVD> GetAll();
        DVD Add(DVD dvd);
        void Update(DVD dvd);
        void Delete(int id);

        DVD SearchByID(int id);
        List<DVD> SearchByTitle(string title);
        
        List<DVD> SearchByDirector(string directorName);
        List<DVD> SearchByRating(string rating);
        List<DVD> SearchByReleaseYear(int year);
    }
}
