using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_ModeOfPaymentRepository : Repository<AC_ModeOfPayment>
    {
        private AppDbContext _context;
        public AC_ModeOfPaymentRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_ModeOfPayment Update(AC_ModeOfPayment obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
