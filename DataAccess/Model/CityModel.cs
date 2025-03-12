using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class CityModel
    {
        public Master_Country CM { get; set; }
        public Master_State SM { get; set; }
        public Master_City CTY { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string UUID { get; set; }

        public CityModel()
        {
            CM = new Master_Country();
            SM = new Master_State();
            CTY = new Master_City();
        }

    }
}
