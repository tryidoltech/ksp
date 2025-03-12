using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_Employee_AssestsDetailsRepository : Repository<Master_Employee_AssestsDetails>
    {
        private AppDbContext _context;
        public Master_Employee_AssestsDetailsRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Employee_AssestsDetails Update(Master_Employee_AssestsDetails obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}