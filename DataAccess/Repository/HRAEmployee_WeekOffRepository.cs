using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRAEmployee_WeekOffRepository : Repository<HRAEmployee_WeekOff>
    {
        private AppDbContext _context;
        public HRAEmployee_WeekOffRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRAEmployee_WeekOff Update(HRAEmployee_WeekOff obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
