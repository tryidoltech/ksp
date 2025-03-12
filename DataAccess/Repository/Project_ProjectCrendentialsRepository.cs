using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Project_ProjectCrendentialsRepository : Repository<Project_ProjectCrendentials>
    {
        private AppDbContext _context;
        public Project_ProjectCrendentialsRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Project_ProjectCrendentials Update(Project_ProjectCrendentials obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
