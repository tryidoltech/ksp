using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Sales_ManagePositiveLeadRepository : Repository<Sales_ManagePositiveLead>
    {
        private AppDbContext _context;
        public Sales_ManagePositiveLeadRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Sales_ManagePositiveLead Update(Sales_ManagePositiveLead obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
