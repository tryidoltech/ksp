
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class ManagePageResourseModel
    {
        public Project_CreateProject CM { get; set; }
        public Master_Designation SM { get; set; }
        public Project_ProjectResources CTY { get; set; }

        public string Project_Name { get; set; }
        public string Designation_Name { get; set; }
        public string Employee_Name { get; set; }

        public ManagePageResourseModel()
        {
            CM = new Project_CreateProject();
            SM = new Master_Designation();
            CTY = new Project_ProjectResources();
        }

    }
}
