using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class ProjectPhaseModel
    {
        public Project_CreateProjectPhase PM { get; set; }
        public Project_CreateProject CM { get; set; }
        public Master_Employee SM { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Project_Title { get; set; }
        public ProjectPhaseModel()
        {
            PM = new Project_CreateProjectPhase();
            CM = new Project_CreateProject();
            SM = new Master_Employee();

        }

    }
}
