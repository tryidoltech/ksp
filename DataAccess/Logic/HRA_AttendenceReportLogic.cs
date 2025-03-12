using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class HRA_AttendenceReportLogic : HRA_AttendenceReportRepository
    {
        private AppDbContext _context;
        public HRA_AttendenceReportLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
