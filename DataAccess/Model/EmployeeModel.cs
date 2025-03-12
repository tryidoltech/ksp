using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class EmployeeModel
    {
        public Master_Employee EM;
        public Master_Honorific HM { get; set; }
        public Master_BloodGroup BM { get; set; }
        public Master_Department DM { get; set; }
        public Master_Gender GM { get; set; }
        public Master_Country CM { get; set; }
        public Master_State SM { get; set; }
        public Master_City CTY { get; set; }

        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string PrefixName { get; set; }
        public string BloodGroupName { get; set; }
        public string DepartmentName { get; set; }
        public string GenderName { get; set; }
        public string UUID { get; set; }

        public EmployeeModel()
        {
            EM = new Master_Employee();
            HM = new Master_Honorific();
            BM = new Master_BloodGroup();
            DM = new Master_Department();
            GM = new Master_Gender();
            CM = new Master_Country();
            SM = new Master_State();
            CTY = new Master_City();
        }

    }
}
