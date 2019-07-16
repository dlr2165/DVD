using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DVDWebAPI.Models.Data;
using DVDLibraryWebAPI.Data;
namespace DVDWebAPI.Data.Repositories.Mock
{
    public class DVDRepositoryADO : IDVDRepository
    {
        public DVDRepositoryADO()
        {
        }
        public DVD Add(DVD dvd)
        {
            int id;
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "sp_DVDInsert"
                };
                cmd.Parameters.AddWithValue("@title", dvd.title);
                cmd.Parameters.AddWithValue("@releaseYear", dvd.releaseYear);
                cmd.Parameters.AddWithValue("@directorID", dvd.directorID);
                cmd.Parameters.AddWithValue("@ratingID", dvd.ratingID);
                cmd.Parameters.AddWithValue("@notes", dvd.notes);
                conn.Open();
                 id =cmd.ExecuteNonQuery();
            }
            DVD newDVD = SearchByID(id);
            return newDVD;

        }
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[sp_DVDDelete]"
                };

                cmd.Parameters.AddWithValue("@dvdid", id);
                conn.Open();
            }
        }
        public List<DVD> GetAll()
        {
            List<DVD> dvd = new List<DVD>();
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "sp_DVDSelectDetail"
                };
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DVD currentRow = new DVD()
                        {
                            dvdID = (int)dr["dvdID"],
                            title = dr["title"].ToString(),
                            releaseYear = (int)dr["releaseYear"],
                            directorID = (int)dr["directorID"],
                            directorFirstName = dr["firstName"].ToString(),
                            directorLastName = dr["lastName"].ToString(),
                            ratingID = (int)dr["ratingID"],
                            rating = dr["rating"].ToString(),
                            notes = dr["notes"].ToString()
                        };
                        dvd.Add(currentRow);
                    }
                }
            }
            return dvd;
        }
        public List<DVD> SearchByReleaseYear(int year)
        {
            List<DVD> dvd = new List<DVD>();
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "sp_SelectDVDByYear"
                };
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DVD currentRow = new DVD()
                        {
                            dvdID = (int)dr["dvdID"],
                            title = dr["title"].ToString(),
                            releaseYear = (int)dr["releaseYear"],
                            directorID = (int)dr["directorID"],
                            directorFirstName = dr["firstName"].ToString(),
                            directorLastName = dr["lastName"].ToString(),
                            ratingID = (int)dr["ratingId"],
                            rating = dr["rating"].ToString(),
                            notes = dr["notes"].ToString()
                        };
                        dvd.Add(currentRow);
                    }
                }
            }
            return dvd;
        }
        public List<DVD> SearchByDirector(string directorName)
        {
            return getDVDList("sp_SelectDVDByDirector","@directorName", directorName);
        }
        public DVD SearchByID(int id)
        {
            DVD dvd = new DVD();
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "sp_SelectDVDByID"
                };
                cmd.Parameters.AddWithValue("@dvdID", id);
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DVD currentRow = new DVD()
                        {
                            dvdID = (int)dr["dvdID"],
                            title = dr["title"].ToString(),
                            releaseYear = (int)dr["releaseYear"],
                            directorID = (int)dr["directorID"],
                            directorFirstName = dr["firstName"].ToString(),
                            directorMiddleName = dr["middleName"].ToString(),
                            directorLastName = dr["lastName"].ToString(),
                            ratingID = (int)dr["ratingID"],
                            rating = dr["rating"].ToString(),
                            notes = dr["notes"].ToString()
                        };
                        dvd = currentRow;
                    }
                }
                dvd.director = getDirectors();
                dvd.ratings = getRatings();
            }
            return dvd;
        }
        private List<Director> getDirectors()
        {
            List<Director> director = new List<Director>();
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[sp_DirectorSelect]"
                };
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Director currentRow = new Director()
                        {
                            directorID = (int)dr["directorID"],
                            director = dr["director"].ToString(),
                        };
                        director.Add(currentRow);
                    }
                }
            }
            return director;
        }
        private List<Ratings> getRatings()
        {
            List<Ratings> rating = new List<Ratings>();
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[sp_RatingSelect]"
                };
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Ratings currentRow = new Ratings()
                        {
                            ratingID = (int)dr["ratingID"],
                            rating = dr["rating"].ToString(),
                        };
                        rating.Add(currentRow);
                    }
                }
            }
            return rating;
        }
        public List<DVD> SearchByRating(string rating)
        {
            return getDVDList("sp_SelectDVDByRating", "@rating", rating);
        }
        public List<DVD> SearchByTitle(string title)
        {
            return getDVDList("sp_SelectDVDByTitle", "@title", title);
        }
        public void Update(DVD dvd)
        {
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "sp_DVDUpdate"
                };
                cmd.Parameters.AddWithValue("@dvdID", dvd.dvdID);
                cmd.Parameters.AddWithValue("@title", dvd.title);
                cmd.Parameters.AddWithValue("@releaseYear", dvd.releaseYear);
                cmd.Parameters.AddWithValue("@directorID", dvd.directorID);
                cmd.Parameters.AddWithValue("@ratingID", dvd.ratingID);
                cmd.Parameters.AddWithValue("@notes", dvd.notes);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        private List<DVD> getDVDList(string storedProcName,string paramName,string searchTerm)
        {
            List<DVD> dvd = new List<DVD>();
            using (SqlConnection conn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand()
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = storedProcName
                };
                cmd.Parameters.AddWithValue(paramName, searchTerm);
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DVD currentRow = new DVD()
                        {
                            dvdID = (int)dr["dvdID"],
                            title = dr["title"].ToString(),
                            releaseYear = (int)dr["releaseYear"],
                            directorID = (int)dr["directorID"],
                            directorFirstName = dr["firstName"].ToString(),
                            directorLastName = dr["lastName"].ToString(),
                            ratingID = (int)dr["ratingId"],
                            rating = dr["rating"].ToString(),
                            notes = dr["notes"].ToString()
                        };
                        dvd.Add(currentRow);
                    }
                }
            }
            return dvd;
        }
    }
}
//@dvdID int,
//	@title varchar(50),
//	@releaseYear int,
//	@directorID int,
//	@ratingID int,
//	@notes varchar(152)