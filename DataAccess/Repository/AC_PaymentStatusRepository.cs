using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_PaymentStatusRepository : Repository<AC_PaymentStatus>
    {
        private AppDbContext _context;
        public AC_PaymentStatusRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_PaymentStatus Update(AC_PaymentStatus obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
