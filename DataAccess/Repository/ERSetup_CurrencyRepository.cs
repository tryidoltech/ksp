using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ERSetup_CurrencyRepository : Repository<ERSetup_Currency>
    {
        private AppDbContext _context;
        public ERSetup_CurrencyRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ERSetup_Currency Update(ERSetup_Currency obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
