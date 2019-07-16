using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DVDWebAPI.EF;
using DVDWebAPI.Models.Data;
namespace DVDWebAPI.Data.Repositories.Entity_Framework
{
    public class DVDRepositoryEF : IDVDRepository
    {
        public Models.Data.DVD Add(Models.Data.DVD dvd)
        {
            Models.Data.DVD d1 = new Models.Data.DVD();
            Entities ef = new Entities();
            var newDVDId = ef.sp_DVDInsert(dvd.title,dvd.releaseYear,dvd.directorID,dvd.ratingID,dvd.notes);
            d1 = SearchByID(Convert.ToInt32(newDVDId));
            d1.ratings = getRatings();
            d1.director = getDirectors();
            return d1;
        }
        public void Delete(int id)
        {
            Entities ef = new Entities();
            ef.sp_DVDDelete(id);
        }
        public List<Models.Data.DVD> GetAll()
        {
            List<Models.Data.DVD> dvd = new List<Models.Data.DVD>();
           Entities  ef = new Entities();
            try
            {
                var ef1 = ef.sp_DVDSelectdetail(0);
                foreach (var item in ef1)
                {
                    Models.Data.DVD currentRow = new Models.Data.DVD()
                    {
                        dvdID = (int)item.dvdID,
                        title = item.title,
                        releaseYear = item.releaseYear,
                        directorID = item.directorID,
                        directorFirstName = item.firstName,
                        directorMiddleName = item.middleName,
                        directorLastName = item.lastName,
                        ratingID = item.ratingID,
                        rating = item.rating,
                        notes = item.notes
                    };
                    dvd.Add(currentRow);
                }
            }
            catch(Exception ex)
            {
            }
            return dvd;
            // throw new NotImplementedException();
        }
        public List<Models.Data.DVD> SearchByDirector(string directorName)
        {
            throw new NotImplementedException();
        }
        public Models.Data.DVD SearchByID(int id)
        {
            Models.Data.DVD dvd = new Models.Data.DVD();
            Entities e = new Entities();
            var d1 = e.sp_SelectDVDByID(id);
            foreach (var item in d1) {
                Models.Data.DVD current = new Models.Data.DVD()
                {
                    dvdID = item.dvdID,
                    title = item.title,
                    releaseYear = item.releaseYear,
                    directorID = item.directorID,
                    directorFirstName = item.firstName,
                    directorMiddleName = item.middleName,
                    directorLastName = item.lastName,
                    ratingID = item.ratingID,
                    rating = item.rating,
                    notes = item.notes
                };
                dvd = current;
            }
            dvd.ratings = getRatings();
            dvd.director = getDirectors();
            return dvd;
        }
        public List<Models.Data.DVD> SearchByRating(string rating)
        {
            throw new NotImplementedException();
        }
        public List<Models.Data.DVD> SearchByReleaseYear(int year)
        {
            throw new NotImplementedException();
        }
        public List<Models.Data.DVD> SearchByTitle(string title)
        {
            throw new NotImplementedException();
        }
        public void Update(Models.Data.DVD dvd)
        {
            throw new NotImplementedException();
        }
        private List<Models.Data.Director> getDirectors()
        {
            Entities e= new Entities();
            List<Models.Data.Director> director = new List<Models.Data.Director>();
            var efDir = e.sp_DirectorSelect(0);
            foreach (var item in efDir)
            {
                Models.Data.Director currentRow = new Models.Data.Director()
                {
                    directorID = item.directorID,
                    director = item.director
                };
                director.Add(currentRow);
            }
            return director;
        }
        private List<Models.Data.Ratings> getRatings()
        {
            Entities e = new Entities();
            List<Models.Data.Ratings> rating = new List<Models.Data.Ratings>();
            var efDir = e.sp_RatingSelect(0);
            foreach (var item in efDir)
            {
                Models.Data.Ratings currentRow = new Models.Data.Ratings()
                {
                    ratingID = item.ratingID,
                    rating = item.rating
                };
                rating.Add(currentRow);
            }
            return rating;
        }
    }
}