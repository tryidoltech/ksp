using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Project_ManageTaskTimeLineRepository : Repository<Project_ManageTaskTimeLine>
    {
        private AppDbContext _context;
        public Project_ManageTaskTimeLineRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Project_ManageTaskTimeLine Update(Project_ManageTaskTimeLine obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
