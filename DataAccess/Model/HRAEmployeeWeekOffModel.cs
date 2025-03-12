using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class HRAEmployeeWeekOffModel
    {

        public Master_CompanyBranch CB { get; set; }
        public Master_Employee ME { get; set; }
        public HRA_WeekDay WD { get; set; }
        public HRAEmployee_WeekOff OWN { get; set; }    
        public string UUID { get; set; }
        public string FirstName { get; set; }
        public string Branch_Name { get; set; }
        public string LastName { get; set; }
        public string WeekDay_Name { get; set; }
       

        public HRAEmployeeWeekOffModel()
        {

            CB = new Master_CompanyBranch();
            ME = new Master_Employee();
            WD = new HRA_WeekDay();
            OWN = new HRAEmployee_WeekOff();
            
        }

    }
}
