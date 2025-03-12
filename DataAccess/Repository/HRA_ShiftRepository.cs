using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRA_ShiftRepository : Repository<HRA_Shift>
    {
        private AppDbContext _context;
        public HRA_ShiftRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRA_Shift Update(HRA_Shift obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
