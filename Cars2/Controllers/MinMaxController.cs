using Cars2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cars2.Controllers
{
    /// <summary>
    /// Returns information about cars in the HCL2 Cars db, within the specified min/max range
    /// </summary>
    [RoutePrefix("CarsWith")]
    public class MinMaxController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Two ways of specifying the route:
        // If we want to say /carswith/numcyls/10/12... then use:
        //  [Route("numcyls/{min}/{max}
        //
        // But if we want to say /carswith/numcyls?min=10&max=12... then use
        //  [Route("numcyls")]
        // and specify the vars on the URL line
        /// <summary>
        /// Return list of cars with the number of cylinders between min and max (inclusive)
        /// </summary>
        /// <param name="min">Minimum number of cylinders</param>
        /// <param name="max">Maximum number of cylinders</param>
        /// <returns></returns>
        [Route("numcyls")]
        public IEnumerable<Car> GetCarsBetweenNumCyl(int min, int max = 1000)
        {
            var retval = db.Database.SqlQuery<Car>("EXEC GetCarsBetweenNumCyl @min, @max",
                new SqlParameter("min", min),
                new SqlParameter("max", max));
            return retval.ToArray<Car>();
        }

        /// <summary>
        /// Return list of cars with horsepower between min and max (inclusive)
        /// </summary>
        /// <param name="min">Minimum horsepower</param>
        /// <param name="max">Maximum horsepower</param>
        /// <returns></returns>
        [Route("hp")]
        public IEnumerable<Car> GetCarsBetweenHP(int min, int max = 1999)
        {
            var retval = db.Database.SqlQuery<Car>("EXEC GetCarsBetweenHP @min, @max",
                new SqlParameter("min", min),
                new SqlParameter("max", max));
            return retval.ToArray<Car>();
        }

    }
}
