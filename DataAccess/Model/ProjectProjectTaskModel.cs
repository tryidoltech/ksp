using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Model;

namespace DataAccess.Model
{
    public class ProjectProjectTaskModel
    {
        public Project_CreateProject CM { get; set; }
        public Project_CreateProjectPhase SM { get; set; }
        public Project_ProjectTask CTY { get; set; }

        public string Project_Title { get; set; }
        public string Phase_Name { get; set; }
        public string Task_Title { get; set; }
        public string UUID { get; set; }



        public ProjectProjectTaskModel()
        {
            CM = new Project_CreateProject();
            SM = new Project_CreateProjectPhase();
            CTY = new Project_ProjectTask();

        }
    }
}





