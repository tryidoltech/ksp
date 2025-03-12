using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_PaymentMeansCodeRepository : Repository<AC_PaymentMeansCode>
    {
        private AppDbContext _context;
        public AC_PaymentMeansCodeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_PaymentMeansCode Update(AC_PaymentMeansCode obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
