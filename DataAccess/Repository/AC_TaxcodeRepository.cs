using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_TaxcodeRepository : Repository<AC_Taxcode>
    {
        private AppDbContext _context;
        public AC_TaxcodeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_Taxcode Update(AC_Taxcode obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
