using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Project_CreateProjectPhaseRepository : Repository<Project_CreateProjectPhase>
    {
        private AppDbContext _context;
        public Project_CreateProjectPhaseRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Project_CreateProjectPhase Update(Project_CreateProjectPhase obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
