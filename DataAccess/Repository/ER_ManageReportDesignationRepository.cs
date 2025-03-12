using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ER_ManageReportDesignationRepository : Repository<ER_ManageReportDesignation>
    {
        private AppDbContext _context;
        public ER_ManageReportDesignationRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ER_ManageReportDesignation Update(ER_ManageReportDesignation obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
