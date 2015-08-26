using Cars2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Cars2.Controllers
{
    /// <summary>
    /// Return lists of years, makes, models, and trims; used to populate selection dropdowns
    /// </summary>
    [RoutePrefix("api")]
    public class CarsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Get all distinct years for cars in the HCL2.Cars database
        /// </summary>
        /// <returns>List of all years having at least one car in the database</returns>
        [Route("years")]
        public IEnumerable<string> GetYears()
        {
            var retval = db.Database.SqlQuery<string>("EXEC GetYears");
            return retval;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IEnumerable<string> GetDontShowThisMethod(string a, string b, string c, string d, string e)
        {
            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IEnumerable<string> GetButShowThisOne(string a, string b, string c, string d, string e, string f)
        {
            return null;
        }

        /// <summary>
        /// Get all distinct car makes for the specified model year
        /// </summary>
        /// <param name="year">The model year</param>
        /// <returns>List of makes for the specified model year</returns>
        [Route("makes")]
        public IEnumerable<string> GetYearMakes(int year)
        {
            var retval = db.Database.SqlQuery<string>("EXEC GetYearMakes @year",
                new SqlParameter("year", year));
            return retval;
        }

        /// <summary>
        /// Get all distinct car models for specified year and make
        /// </summary>
        /// <param name="year">The model year</param>
        /// <param name="make">The make (manufacturer)</param>
        /// <returns>List of models for the specified model year and make</returns>
        [Route("models")]
        public IEnumerable<string> GetYearMakeModels(int year, string make)
        {
            var retval = db.Database.SqlQuery<string>("EXEC GetYearMakeModels @year, @make",
                new SqlParameter("year", year),
                new SqlParameter("make", make));
            return retval;
        }

        /// <summary>
        /// Get all distinct trims for the specified model year, make, and model. And something else.
        /// </summary>
        /// <param name="year">The model year</param>
        /// <param name="make">The make (manufacturer)</param>
        /// <param name="model">The model</param>
        /// <returns>List of trims for the specified model year, make, and model</returns>
        [Route("trims")]
        public IEnumerable<string> GetYearMakeModelTrims(int year, string make, string model)
        {
            var retval = db.Database.SqlQuery<string>("EXEC GetYearMakeModelTrims @year, @make, @model",
                new SqlParameter("year", year),
                new SqlParameter("make", make),
                new SqlParameter("model", model));
            return retval;
        }
    }
}
