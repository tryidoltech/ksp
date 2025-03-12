using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BD_ResearchChannelTypeRepository : Repository<BD_ResearchChannelType>
    {
        private AppDbContext _context;
        public BD_ResearchChannelTypeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override BD_ResearchChannelType Update(BD_ResearchChannelType obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
