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
    public class Project_ProjectCrendentialsLogic : Project_ProjectCrendentialsRepository
    {
        private AppDbContext _context;
        public Project_ProjectCrendentialsLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<ProjectCrendentialsModel> getProjectCrendentialSubModel()
        {
            List<ProjectCrendentialsModel> CSM = (from c in _context.Project_ProjectCrendentials.Where(q => q.IsActive == true)
                                            join s in _context.Project_CreateProject.Where(s => s.IsActive == true)
                                                on c.Project_UUID equals s.UUID
                                           
                                            select new ProjectCrendentialsModel
                                            {
                                                CM = s,
                                                CTY = c,
                                                Project_Title = s.Project_Title,
                                            }).ToList();
            return CSM;
        }
    }
}
