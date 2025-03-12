using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ER_ExpenseTypeRepository : Repository<ER_ExpenseType>
    {
        private AppDbContext _context;
        public ER_ExpenseTypeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ER_ExpenseType Update(ER_ExpenseType obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
