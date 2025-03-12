using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Project_ProjectTaskRepository : Repository<Project_ProjectTask>
    {
        private AppDbContext _context;
        public Project_ProjectTaskRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Project_ProjectTask Update(Project_ProjectTask obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
