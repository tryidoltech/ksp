using DataAccess.Entities;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class ProjectDocumentModel
    {
        public Project_CreateProject PC { get; set; }
        public Project_ProjectDocument PD { get; set; }

        public string Project_Title { get; set; }
        public string Title { get; set; }
        public string UUID { get; set; }




        public ProjectDocumentModel()
        {
            PC = new Project_CreateProject();
            PD = new Project_ProjectDocument();
        }

    }
}

