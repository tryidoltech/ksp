using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_TermsOfPaymentRepository : Repository<AC_TermsOfPayment>
    {
        private AppDbContext _context;
        public AC_TermsOfPaymentRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_TermsOfPayment Update(AC_TermsOfPayment obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
