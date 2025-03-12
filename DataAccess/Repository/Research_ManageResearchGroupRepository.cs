using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Research_ManageResearchGroupRepository : Repository<Research_ManageResearchGroup>
    {
        private AppDbContext _context;
        public Research_ManageResearchGroupRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Research_ManageResearchGroup Update(Research_ManageResearchGroup obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
