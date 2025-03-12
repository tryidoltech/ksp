using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_ITSubdeductionRepository : Repository<AC_ITSubdeduction>
    {
        private AppDbContext _context;
        public AC_ITSubdeductionRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_ITSubdeduction Update(AC_ITSubdeduction obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
