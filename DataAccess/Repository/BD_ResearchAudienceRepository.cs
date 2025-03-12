using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BD_ResearchAudienceRepository : Repository<BD_ResearchAudience>
    {
        private AppDbContext _context;
        public BD_ResearchAudienceRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override BD_ResearchAudience Update(BD_ResearchAudience obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
