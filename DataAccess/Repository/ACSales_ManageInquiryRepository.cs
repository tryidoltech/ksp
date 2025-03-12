using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ACSales_ManageInquiryRepository : Repository<ACSales_ManageInquiry>
    {
        private AppDbContext _context;
        public ACSales_ManageInquiryRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ACSales_ManageInquiry Update(ACSales_ManageInquiry obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
