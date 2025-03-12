using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_CompanyRepository : Repository<Master_Company>
    {
        private AppDbContext _context;
        public Master_CompanyRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Company Update(Master_Company obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
