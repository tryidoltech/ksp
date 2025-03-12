using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Project_ProjectStatusRepository : Repository<Project_ProjectStatus>
    {
        private AppDbContext _context;
        public Project_ProjectStatusRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Project_ProjectStatus Update(Project_ProjectStatus obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
