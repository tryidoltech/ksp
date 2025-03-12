using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_ModeOfTransportRepository : Repository<AC_ModeOfTransport>
    {
        private AppDbContext _context;
        public AC_ModeOfTransportRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_ModeOfTransport Update(AC_ModeOfTransport obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
