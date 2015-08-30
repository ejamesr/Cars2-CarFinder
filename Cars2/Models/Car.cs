using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars2.Models
{
    /// <summary>
    /// Car fields for each row in the HCL2 db.
    /// Note: the data in HCL2 is not clean, many columns are missing data, some data is incorrect.
    /// Use at your own risk!
    /// </summary>
    public class Car
    {
        public const double LitersToGallons = 0.26417205236;
        public const double KmToMiles =  0.62137119224;
        public const double KgToPounds = 2.2046226218;
        public const double MmToIn =     0.03937007874;

        // Columns in db, with # occurences
        public int id { get; set; }                         // 60506
        public string make { get; set; }                    // 60506
        public string model_name { get; set; }              // 60506
        public string model_trim { get; set; }              // 47408
        public string model_year { get; set; }              // 60506
        public string body_style { get; set; }              // 44935
        public string engine_position { get; set; }         // 57172
        public string engine_cc { get; set; }               // 60175
        public string engine_num_cyl { get; set; }          // 59108
        public string engine_type { get; set; }             // 59391
        public string engine_valves_per_cyl { get; set; }   // 43201
        /// <summary>
        /// Engine horsepower
        /// </summary>
        public string engine_power_ps { get; set; }         // 56532
        public string engine_power_rpm { get; set; }        // 51408
        public string engine_torque_nm { get; set; }        // 54567
        public string engine_torque_rpm { get; set; }       // 49137
        public string engine_bore_mm { get; set; }          // 31373
        public string engine_stroke_mm { get; set; }        // 31346
        public string engine_compression { get; set; }      // 30641
        public string engine_fuel { get; set; }             // 52100
        public string top_speed_kph { get; set; }           // 19962
        public string zero_to_100_kph { get; set; }         // 20495
        public string drive_type { get; set; }              // 58478
        public string transmission_type { get; set; }       // 50907
        public string seats { get; set; }                   // 51171
        public string doors { get; set; }                   // 50058
        public string weight_kg { get; set; }               // 55670
        public string length_mm { get; set; }               // 57885
        public string width_mm { get; set; }                // 55265
        public string height_mm { get; set; }               // 54929
        public string wheelbase { get; set; }               // 55554
        /// <summary>
        /// Liters per kilometer on the highway
        /// </summary>
        public string lkm_hwy { get; set; }                 // 14401
        /// <summary>
        /// Liters per kilometer in mixed city/hwy driving
        /// </summary>
        public string lkm_mixed { get; set; }               // 25498
        /// <summary>
        /// Liters per kilometer in city driving
        /// </summary>
        public string lkm_city { get; set; }                // 14289
        public string fuel_capacity_l { get; set; }         // 41902
        public string sold_in_us { get; set; }              // 60496
        public string co2 { get; set; }                     // 39
        /// <summary>
        /// A prettier version of the car make (not available for all fields)
        /// </summary>
        public string make_display { get; set; }            // 59158
        /// <summary>
        /// Number of cylinders (as an int)
        /// </summary>
        [NotMapped]
        public int cyl { get; set; }                        // see engine_num_cyl
        /// <summary>
        /// Horsepower (as an int)
        /// </summary>
        [NotMapped]
        public int hp { get; set; }                         // see engine_power_ps

        /*
         * Here are all the columns sorted by # occurrences in db table:
         * 
         * public int id { get; set; }                         // 60506
         * public string make { get; set; }                    // 60506
         * public string model_name { get; set; }              // 60506
         * public string model_year { get; set; }              // 60506
         * public string sold_in_us { get; set; }              // 60496
         * public string engine_cc { get; set; }               // 60175
         * public string engine_type { get; set; }             // 59391
         * public string make_display { get; set; }            // 59158
         * public string engine_num_cyl { get; set; }          // 59108
         * public string drive_type { get; set; }              // 58478
         * public string length_mm { get; set; }               // 57885
         * public string engine_position { get; set; }         // 57172
         * public string engine_power_ps { get; set; }         // 56532
         * public string weight_kg { get; set; }               // 55670
         * public string wheelbase { get; set; }               // 55554
         * public string width_mm { get; set; }                // 55265
         * public string height_mm { get; set; }               // 54929
         * public string engine_torque_nm { get; set; }        // 54567
         * public string engine_fuel { get; set; }             // 52100
         * public string engine_power_rpm { get; set; }        // 51408
         * public string seats { get; set; }                   // 51171
         * public string transmission_type { get; set; }       // 50907
         * public string doors { get; set; }                   // 50058
         * public string engine_torque_rpm { get; set; }       // 49137
         * public string model_trim { get; set; }              // 47408
         * public string body_style { get; set; }              // 44935
         * public string engine_valves_per_cyl { get; set; }   // 43201
         * public string fuel_capacity_l { get; set; }         // 41902
         * public string engine_bore_mm { get; set; }          // 31373
         * public string engine_stroke_mm { get; set; }        // 31346
         * public string engine_compression { get; set; }      // 30641
         * public string lkm_mixed { get; set; }               // 25498
         * public string zero_to_100_kph { get; set; }         // 20495
         * public string top_speed_kph { get; set; }           // 19962
         * public string lkm_hwy { get; set; }                 // 14401
         * public string lkm_city { get; set; }                // 14289
         * public string co2 { get; set; }                     // 39
         * 
         * */

        public string MpgHwy()
        {
            return "mpg hwy";
        }
        public string MpgMixed()
        {
            return "mpg mixed";
        }
        public string MpgCity()
        {
            return "mpg city";
        }
        public string FuelCapacity()
        {
            return "gals";
        }
        public string WeightPounds()
        {
            return "pounds";
        }

    }

}