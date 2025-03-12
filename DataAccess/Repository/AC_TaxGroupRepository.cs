using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_TaxGroupRepository : Repository<AC_TaxGroup>
    {
        private AppDbContext _context;
        public AC_TaxGroupRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_TaxGroup Update(AC_TaxGroup obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
