using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRA_NomineeRepository : Repository<HRA_Nominee>
    {
        private AppDbContext _context;
        public HRA_NomineeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRA_Nominee Update(HRA_Nominee obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
