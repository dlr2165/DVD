using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVDWebAPI.Models.Data;
using DVDWebAPI.Models;
using DVDWebAPI.Data;
namespace DVDWebAPI.Data.Repositories.Mock
{
    //mock / test repository
    public class DVDRepositoryMock : IDVDRepository
    {
        //private list because this list is not used outside of this class
        private List<DVD> _dvds;
        private List<Director> _director;
        private List<Ratings> _rating;
        public DVDRepositoryMock()
        {//populate static data
            _dvds = new List<DVD>()
            {
                new DVD { dvdID = 1,title = "Scream",releaseYear = 1998,directorID = 2,ratingID = 4,notes = "knife weilding psychopath on his way to kill them kids"},
                new DVD { dvdID = 2,title = "The Conjuring",releaseYear = 2013,directorID = 1,ratingID = 3,notes = "family moves into a seculded, haunted farm house"},
                new DVD { dvdID = 3,title = "Young Frankenstein",releaseYear = 1974,directorID = 3,ratingID = 2,notes = "dr. Frankenstein creates a zombie. monester or not?"},
                new DVD { dvdID = 4,title = "Nightmare on Elm Street",releaseYear = 1984,directorID = 2,ratingID = 4,notes = "dream demon kills in your sleep"},
                new DVD { dvdID = 5,title = "Transformers",releaseYear = 2007,directorID = 4,ratingID = 3,notes = "two opposing robot factions show up on earth to battle"},
                new DVD { dvdID = 6,title = "Stripes",releaseYear = 1981,directorID = 5,ratingID = 4,notes = "two friends are dissatisfied with everyday life, join Army"},
                new DVD { dvdID = 7,title = "Armageddon",releaseYear = 1998,directorID = 4,ratingID = 3,notes = "a group of oil workers are sent to space to destroy a meteor"},
                new DVD { dvdID = 8,title = "Blazing Saddles",releaseYear = 1974,directorID = 3,ratingID = 4,notes = "railroad worker turned sherrif helps town against bandits"},
                new DVD { dvdID = 9,title = "Aquaman",releaseYear = 2018,directorID = 1,ratingID = 3,notes = "human born heir to atlantis stops war between ocean and land"},
                new DVD { dvdID = 10,title = "Ghost Busters",releaseYear = 1984,directorID = 5,ratingID = 2,notes = "three former parapsychology professors hunt ghosts"}
             };
            _director = new List<Director>
            {
                 new Director{ directorID = 1, director = "James Wan"},
                 new Director{ directorID = 2, director = "Wes Earl Craven"},
                 new Director{ directorID = 3, director = "Mel Brooks"},
                 new Director{ directorID = 4, director = "Michael Benjamin Bay"},
                 new Director{ directorID = 5, director = "Ivan Reitman"}
            };
            _rating = new List<Ratings>
            {
               new Ratings{ratingID = 1, rating = "G"},
               new Ratings{ratingID = 2, rating = "PG"},
               new Ratings{ratingID = 3, rating = "PG-13"},
               new Ratings{ratingID = 4, rating = "R"}
            };
        }
        //Methods from interface
        public DVD Add(DVD dvd)
        {
            dvd.dvdID = _dvds.Max(d => d.dvdID) + 1;
            _dvds.Add(dvd);
            return dvd;
        }
        public void Delete(int id)
        {
            _dvds.RemoveAll(d => d.dvdID == id);
        }
        public List<DVD> GetAll()
        {


            var dvds = _dvds.Join(_director,
                                 dv => dv.directorID,
                                 dir => dir.directorID,
                                 (dv, dir) => dv);
            return (List<DVD>) dvds;
        }
        public DVD GetById(int id)
        {
            return _dvds.FirstOrDefault(d => d.dvdID == id);
        }
        public List<DVD> SearchByDirector(string directorName)
        {
            List<Director> directors = new List<Director>();
            directors = _director.FindAll(d => d.director == directorName);
            List<DVD> dvds = new List<DVD>();
            foreach (DVD dvd in _dvds)
            {
               foreach(Director dir in directors)
                {
                    if(dir.directorID  == dvd.directorID)
                    {
                        dvds.Add(dvd);
                    }
                }
            }
            return dvds.ToList();
        }
        public DVD SearchByID(int id)
        {

            DVD dvd = _dvds.FirstOrDefault(d => d.dvdID == id);
            dvd.ratings = _rating;
            dvd.director = _director;
            return dvd;
        }
        public List<DVD> SearchByRating(string rating)
        {
            List<DVD> dvds = new List<DVD>();
            foreach (DVD dvd in _dvds)
            {
                if (dvd.rating.ToUpper() == rating.ToUpper())
                {
                    dvds.Add(dvd);
                }
            }
            return dvds;
        }
        public List<DVD> SearchByReleaseYear(int year)
        {
            List<DVD> dvds = new List<DVD>();
            foreach (DVD dvd in _dvds)
            {
                if (dvd.releaseYear == year)
                {
                    dvds.Add(dvd);
                }
            }
            return dvds;
        }
        public List<DVD> SearchByTitle(string title)
        {
            List<DVD> dvds = new List<DVD>();
            foreach (DVD dvd in _dvds)
            {
                if (dvd.title.ToLower().Contains(title.ToLower()))
                {
                    dvds.Add(dvd);
                }
            }
            return dvds;
        }
        public void Update(DVD dvd)
        {
            var dVD = _dvds.FirstOrDefault(d => d.dvdID == dvd.dvdID);
            if (dVD != null)
            {
                dVD.title = dvd.title;
                dVD.releaseYear = dvd.releaseYear;
                dVD.director = dvd.director;
                dVD.rating = dvd.rating;
                dVD.notes = dvd.notes;
            }
        }
    }
}
