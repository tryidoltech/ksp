using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_YearRepository : Repository<Master_Year>
    {
        private AppDbContext _context;
        public Master_YearRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Year Update(Master_Year obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
