using Cars2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Cars2.Controllers
{
    /// <summary>
    /// Get list of cars meeting specifications
    /// </summary>
    [Route("Cars")]
    public class GetCarsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Get sorted (ASC) list of ids for all cars in HCL2 Cars db
        /// </summary>
        /// <returns>List of all id values (int) for all cars in db</returns>
        [Route("Cars_id")]
        public IEnumerable<int> GetAllCarsIdOnly()
        {
            return db.Database.SqlQuery<int>("EXEC GetAllCarsIdOnly");
        }

        /// <summary>
        /// Get list of all cars in HCL2 Cars db, specific columns only, separated by '~' char
        /// </summary>
        /// <returns>Returns for each row: result of "CONCAT(RTRIM(id), '~', RTRIM(model_year), '~', RTRIM(make), '~', RTRIM(model_name), '~', RTRIM(model_trim))"</returns>
        [Route("Cars_short")]
        public IEnumerable<string> GetAllCarsShort()
        {
            return db.Database.SqlQuery<string>("EXEC GetAllCarsShort");
        }

        /// <summary>
        /// Get list of all columns for all cars in HCL2 Cars db
        /// </summary>
        /// <returns>List of all columns for all cars in db</returns>
        [Route("Cars_long")]
        public IEnumerable<Car> GetAllCarsFull()
        {
            return db.Database.SqlQuery<Car>("EXEC GetAllCarsFull");
        }

        /// <summary>
        /// Get list of cars in HCL2 db for specified year
        /// </summary>
        /// <param name="year">The model year</param>
        /// <returns>List of cars for specified model year</returns>
        public IEnumerable<Car> GetCarsByYear(int year)
        {
            var retval = db.Database.SqlQuery<Car>("EXEC GetCarsByYear @year",
                new SqlParameter("year", year));
            return retval;
        }

        /// <summary>
        /// Get list of cars in HCL2 db for specified year and make
        /// </summary>
        /// <param name="year">The car model year</param>
        /// <param name="make">The car make</param>
        /// <returns>List of cars for specified model year and make</returns>
        public IEnumerable<Car> GetCarsByYearMake(int year, string make)
        {
            var retval = db.Database.SqlQuery<Car>("EXEC GetCarsByYearMake @year, @make",
                new SqlParameter("year", year),
                new SqlParameter("make", make));
            return retval;
        }

        /// <summary>
        /// Get list of cars in HCL2 db for specified year, make, and model
        /// </summary>
        /// <param name="year">The car model year</param>
        /// <param name="make">The car make</param>
        /// <param name="model">The car model</param>
        /// <returns>List of cars for specified model year, make, and model</returns>
        public IEnumerable<Car> GetCarsByYearMakeModel(int year, string make, string model)
        {
            var retval = db.Database.SqlQuery<Car>("EXEC GetCarsByYearMakeModel @year, @make, @model",
                new SqlParameter("year", year),
                new SqlParameter("make", make),
                new SqlParameter("model", model));
            return retval;
        }

        /// <summary>
        /// Get list of cars in HCL2 db for specified year, make, model, and trim
        /// </summary>
        /// <param name="year">The car model year</param>
        /// <param name="make">The car make</param>
        /// <param name="model">The car model</param>
        /// <param name="trim">The trim for that model</param>
        /// <returns>List of cars for specified model year, make, model, and trim</returns>
        public async Task<IHttpActionResult> GetCarsByYearMakeModelTrim(int year, string make, string model, string trim)
        {
            ViewModel viewModel = new ViewModel();
            if (String.IsNullOrEmpty(trim))
            {
                var retval = db.Database.SqlQuery<Car>("EXEC GetCarsByYearMakeModel @year, @make, @model",
                    new SqlParameter("year", year),
                    new SqlParameter("make", make),
                    new SqlParameter("model", model));
                viewModel.cars = retval.ToArray<Car>()[0];
            }
            else
            {
                var retval = db.Database.SqlQuery<Car>("EXEC GetCarsByYearMakeModelTrim @year, @make, @model, @trim",
                    new SqlParameter("year", year),
                    new SqlParameter("make", make),
                    new SqlParameter("model", model),
                    new SqlParameter("trim", trim));
                    viewModel.cars = retval.ToArray<Car>()[0];
            }


            // Get NHTSA info...

            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response;
                    client.BaseAddress = new System.Uri("http://www.nhtsa.gov");
                    response = await client.GetAsync(
                        "webapi/api/recalls/vehicle/modelyear/" + viewModel.cars.model_year.ToString() +
                        "/make/" + viewModel.cars.make +
                        "/model/" + viewModel.cars.model_name +
                        "?format=json");
                    var temp = await response.Content.ReadAsStringAsync();
                    viewModel.recalls = JsonConvert.DeserializeObject(temp);
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }

            // Get images...


            // Because this is async, need to return via Ok()
            return Ok(viewModel);
        }
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public class ViewModel
    {
        public Car cars;
        public object recalls;
        public object images;
    }
}
