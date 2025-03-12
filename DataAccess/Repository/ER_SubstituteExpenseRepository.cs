using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ER_SubstituteExpenseRepository : Repository<ER_SubstituteExpense>
    {
        private AppDbContext _context;
        public ER_SubstituteExpenseRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ER_SubstituteExpense Update(ER_SubstituteExpense obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
