using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_CompanyTypeRepository : Repository<Master_CompanyType>
    {
        private AppDbContext _context;
        public Master_CompanyTypeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_CompanyType Update(Master_CompanyType obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
