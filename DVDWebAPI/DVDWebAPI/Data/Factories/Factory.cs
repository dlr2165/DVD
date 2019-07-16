using DVDWebAPI.Data;
using DVDWebAPI.Data.Repositories.Mock;
using DVDWebAPI.Data.Repositories.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
namespace DVDLibraryWebAPI.Controllers
{   //decides which repository to use, set in our web config file
    public static class DVDRepositoryFactory
    {
        public static IDVDRepository Create()
        {
            var setting = ConfigurationManager.AppSettings["Mode"].ToString();
            switch (setting)
            {
              case "DVDRepositoryMock":
                    return new DVDRepositoryMock();
                case "DVDRepositoryADO":
                    return new DVDRepositoryADO();
                case "DVDRepositoryEF":
                    return new DVDRepositoryEF();
                default:
                    return null;
            }
        }
    }
}