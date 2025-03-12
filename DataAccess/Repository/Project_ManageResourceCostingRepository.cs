using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Project_ManageResourceCostingRepository : Repository<Project_ManageResourceCosting>
    {
        private AppDbContext _context;
        public Project_ManageResourceCostingRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Project_ManageResourceCosting Update(Project_ManageResourceCosting obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
