using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Timesheet_LineRepository : Repository<Timesheet_Line>
    {
        private AppDbContext _context;
        public Timesheet_LineRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Timesheet_Line Update(Timesheet_Line obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
