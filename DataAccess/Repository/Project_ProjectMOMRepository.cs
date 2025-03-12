using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Project_ProjectMOMRepository : Repository<Project_ProjectMOM>
    {
        private AppDbContext _context;
        public Project_ProjectMOMRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Project_ProjectMOM Update(Project_ProjectMOM obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
