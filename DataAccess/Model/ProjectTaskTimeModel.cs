using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class ProjectTaskTimeModel
    {
        public Project_CreateProject CP { get; set; }
        public Project_CreateProjectPhase PP { get; set; }
        public Project_ProjectTask PT { get; set; }
        public Project_ManageTaskTimeLine MT { get; set; }

        public string Project_Title { get; set; }
        public string Phase_Name { get; set; }
        public string Task_Title { get; set; }
        public string UUID { get; set; }
        public string Task_Time_Line_Title { get; set; }

        public ProjectTaskTimeModel()
        {
            CP = new Project_CreateProject();
            PP = new Project_CreateProjectPhase();
            PT = new Project_ProjectTask();
            MT = new Project_ManageTaskTimeLine();
        }

    }
}
