using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRA_WeekDayRepository : Repository<HRA_WeekDay>
    {
        private AppDbContext _context;
        public HRA_WeekDayRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRA_WeekDay Update(HRA_WeekDay obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
