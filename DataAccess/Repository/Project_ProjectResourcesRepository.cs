using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Project_ProjectResourcesRepository : Repository<Project_ProjectResources>
    {
        private AppDbContext _context;
        public Project_ProjectResourcesRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Project_ProjectResources Update(Project_ProjectResources obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
