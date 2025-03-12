using DataAccess.Entities;
using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Project_ManageTaskTimeLineLogic : Project_ManageTaskTimeLineRepository
    {
        private AppDbContext _context;
        public Project_ManageTaskTimeLineLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
        public List<ProjectTaskTimeModel> GetProjectTaskTimelineModel()
        {
            var PTL = (from mt in _context.Project_ManageTaskTimeLine .Where(q => q.IsActive == true)
                       join pc in _context.Project_CreateProject.Where(pc => pc.IsActive == true)
                       on mt.Project_UUID equals pc.UUID
                       join pp in _context.Project_CreateProjectPhase.Where(pp => pp.IsActive == true)
                       on mt.ProjectPhase_UUID equals pp.UUID
                       join pt in _context.Project_ProjectTask.Where(pt => pt.IsActive == true)
                      on mt.ProjectTask_UUID equals pt.UUID
                       select new ProjectTaskTimeModel
                       {
                         CP = pc,
                         PP = pp,
                         PT = pt,
                         MT = mt,
                         UUID = mt.UUID,
                         Project_Title =pc.Project_Title,
                         Phase_Name = pp.Phase_Name,
                         Task_Title = pt.Task_Title,
                         Task_Time_Line_Title = mt.Task_Time_Line_Title
                       }).ToList();

            return PTL;
        }
    }
}
