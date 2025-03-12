using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRA_AttendenceReportRepository : Repository<HRA_AttendenceReport>
    {
        private AppDbContext _context;
        public HRA_AttendenceReportRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRA_AttendenceReport Update(HRA_AttendenceReport obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
