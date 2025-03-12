using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class ManageResourceCostingModel
    {
        public Project_CreateProject PC { get; set; }
        public Master_Employee ME { get; set; }
        public Project_ManageResourceCosting PRC { get; set; }

        public string Project_Title { get; set; }
        public string FullName { get; set; }
        public string UUID { get; set; }

        public ManageResourceCostingModel()
        {
            PC = new Project_CreateProject();
            ME = new Master_Employee();
            PRC = new Project_ManageResourceCosting();
        }
    }
}
