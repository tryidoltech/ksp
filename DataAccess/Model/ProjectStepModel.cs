using DataAccess.Entities;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class ProjectStepModel
    {
        public Project_CreateProject CM { get; set; }
        public Project_CreateProjectPhase SM { get; set; }
        public Project_CreateProjectStep CTY { get; set; }

        public string Project_Title { get; set; }
        public string Phase_Name { get; set; }
        public string Project_Step_Name { get; set; }
        public string UUID { get; set; }

        public ProjectStepModel()
        {
            CM = new Project_CreateProject();
            SM = new Project_CreateProjectPhase();
            CTY = new Project_CreateProjectStep();
        }
    }
}


