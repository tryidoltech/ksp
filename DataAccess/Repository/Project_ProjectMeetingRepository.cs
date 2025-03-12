using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Project_ProjectMeetingRepository : Repository<Project_ProjectMeeting>
    {
        private AppDbContext _context;
        public Project_ProjectMeetingRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Project_ProjectMeeting Update(Project_ProjectMeeting obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
