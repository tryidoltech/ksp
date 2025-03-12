using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ER_HeadDesignationRepository : Repository<ER_HeadDesignation>
    {
        private AppDbContext _context;
        public ER_HeadDesignationRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ER_HeadDesignation Update(ER_HeadDesignation obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
