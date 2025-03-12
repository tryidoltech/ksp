using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BD_ResearchCommunicationModeRepository : Repository<BD_ResearchCommunicationMode>
    {
        private AppDbContext _context;
        public BD_ResearchCommunicationModeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override BD_ResearchCommunicationMode Update(BD_ResearchCommunicationMode obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
