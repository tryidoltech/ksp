using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class CompanyBranchModel
    {

        public Master_CompanyBranch CB { get; set; }
        public Master_Company CN { get; set; }
        public Master_Country CM { get; set; }
        public Master_State SM { get; set; }
        public Master_City CTY { get; set; }

        public string UUID { get; set; }

        public string CompanyName { get; set; }
        public string Branch_Name { get; set; }
        public string Branch_Address { get; set; }

        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }

        public CompanyBranchModel()
        {

            CB = new Master_CompanyBranch();
            CN = new Master_Company();
            CM = new Master_Country();
            SM = new Master_State();
            CTY = new Master_City();
        }

    }
}
