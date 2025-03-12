using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_IndustryRepository : Repository<Master_Industry>
    {
        private AppDbContext _context;
        public Master_IndustryRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Industry Update(Master_Industry obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
