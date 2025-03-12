using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Project_CreateProjectRepository : Repository<Project_CreateProject>
    {
        private AppDbContext _context;
        public Project_CreateProjectRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Project_CreateProject Update(Project_CreateProject obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
