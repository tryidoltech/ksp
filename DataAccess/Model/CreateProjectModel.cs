using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class CreateProjectModel
    {
        public AC_CustomerMaster CM { get; set; }
        public Master_Employee SM { get; set; }
        public Project_CreateProject CTY { get; set; }

        public string CustomerMaster_Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CreateProjectModel()
        {
            CM = new AC_CustomerMaster();
            SM = new Master_Employee();
            CTY = new Project_CreateProject();

        }
    }
}
