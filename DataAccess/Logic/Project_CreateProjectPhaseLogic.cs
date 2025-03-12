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
    public class Project_CreateProjectPhaseLogic : Project_CreateProjectPhaseRepository
    {
        private AppDbContext _context;
        public Project_CreateProjectPhaseLogic(AppDbContext context)
            : base(context)
        {
            _context = context;

        }
        public List<ProjectPhaseModel> getprojectphaseSubModel()
        {
            List<ProjectPhaseModel> CSM = (from c in _context.Project_CreateProjectPhase.Where(q => q.IsActive == true)
                                           join s in _context.Project_CreateProject.Where(s => s.IsActive == true)
                                               on c.Project_UUID equals s.UUID  // UUID matching properly
                                           join p in _context.Master_Employee.Where(e => e.IsActive == true)
                                               on c.Employee_UUID equals p.UUID  // Employee_UUID se join
                                           select new ProjectPhaseModel
                                           {
                                               PM = c,
                                               CM = s,
                                               SM = p,
                                               Project_Title = s.Project_Title,
                                               FirstName = p.FirstName + " " + p.LastName
                                           }).ToList();

            return CSM;
        }

    }
}
