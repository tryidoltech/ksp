using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ER_ExpenseSubTypeRepository : Repository<ER_ExpenseSubType>
    {
        private AppDbContext _context;
        public ER_ExpenseSubTypeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ER_ExpenseSubType Update(ER_ExpenseSubType obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
