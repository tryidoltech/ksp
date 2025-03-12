using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Model
{
    public class ProjectCrendentialsModel
    {
        public Project_CreateProject CM { get; set; }
      
        public Project_ProjectCrendentials CTY { get; set; }

        public string Project_Title { get; set; }
       

        public ProjectCrendentialsModel()
        {
            CM = new Project_CreateProject();
           
            CTY = new Project_ProjectCrendentials();

        }
    }
}
