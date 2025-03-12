using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Timesheet_HeaderLogic : Timesheet_HeaderRepository
    {
        private AppDbContext _context;
        public Timesheet_HeaderLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
