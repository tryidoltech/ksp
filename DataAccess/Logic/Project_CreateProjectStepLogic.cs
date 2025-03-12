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
    public class Project_CreateProjectStepLogic : Project_CreateProjectStepRepository
    {
        private AppDbContext _context;
        public Project_CreateProjectStepLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<ProjectStepModel> getProjectStepSubModel()
        {
            List<ProjectStepModel> CSM = (from c in _context.Project_CreateProjectStep.Where(q => q.IsActive == true)
                                          join s in _context.Project_CreateProjectPhase.Where(s => s.IsActive == true)
                                              on c.ProjectPhase_UUID equals s.UUID
                                          join p in _context.Project_CreateProject.Where(e => e.IsActive == true)
                                              on s.Project_UUID equals p.UUID
                                          select new ProjectStepModel
                                          {
                                              CM = p,
                                              SM = s,
                                              CTY = c,
                                              UUID = c.UUID,
                                              Project_Step_Name = c.Project_Step_Name,
                                              Project_Title = p.Project_Title,
                                              Phase_Name = s.Phase_Name,
                                          }).ToList();
            return CSM;
        }
    }
}



