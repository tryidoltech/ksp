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
    public class Project_ProjectTaskLogic : Project_ProjectTaskRepository
    {
        private AppDbContext _context;
        public Project_ProjectTaskLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<ProjectProjectTaskModel> getProjectTaskSubModel()
        {
            List<ProjectProjectTaskModel> CSM = (from c in _context.Project_ProjectTask.Where(q => q.IsActive == true)
                                            join s in _context.Project_CreateProjectPhase.Where(s => s.IsActive == true)
                                                on c.ProjectPhase_UUID equals s.UUID
                                            join p in _context.Project_CreateProject.Where(e => e.IsActive == true)
                                                on s.Project_UUID equals p.UUID
                                            select new ProjectProjectTaskModel
                                            {
                                                CM = p,
                                                SM = s,
                                                CTY = c,
                                                UUID =c.UUID,
                                                Task_Title = c.Task_Title,
                                                Project_Title = p.Project_Title,
                                                Phase_Name = s.Phase_Name,
                                            }).ToList();
            return CSM;
        }
    }
}





