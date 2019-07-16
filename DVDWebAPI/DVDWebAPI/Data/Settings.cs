using DVDLibraryWebAPI.Controllers;
using DVDWebAPI.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
namespace DVDLibraryWebAPI.Data
{
    //gets connection string from our web config file
    // gets the repository that we are calling from web config file
    public class Settings
    {
        private static IDVDRepository _repo;
        private static string _connectionString;
        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
                _connectionString = ConfigurationManager.ConnectionStrings["DvdDatabase"].ConnectionString;
            return _connectionString;
        }
        public static IDVDRepository GetRepository()
        {
            if (_repo == null)
                _repo = DVDRepositoryFactory.Create();
            return _repo;
        }
    }
}