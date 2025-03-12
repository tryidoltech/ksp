using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_Employee_DocumentSetupRepository : Repository<Master_Employee_DocumentSetup>
    {
        private AppDbContext _context;
        public Master_Employee_DocumentSetupRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Employee_DocumentSetup Update(Master_Employee_DocumentSetup obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
