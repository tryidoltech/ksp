using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Project_CreateProjectStepRepository : Repository<Project_CreateProjectStep>
    {
        private AppDbContext _context;
        public Project_CreateProjectStepRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Project_CreateProjectStep Update(Project_CreateProjectStep obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
