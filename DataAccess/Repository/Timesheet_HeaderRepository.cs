using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Timesheet_HeaderRepository : Repository<Timesheet_Header>
    {
        private AppDbContext _context;
        public Timesheet_HeaderRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Timesheet_Header Update(Timesheet_Header obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
