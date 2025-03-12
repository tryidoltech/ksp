using DataAccess.Entities;
using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class ER_ExpenseSubTypeLogic : ER_ExpenseSubTypeRepository
    {
        private AppDbContext _context;
        public ER_ExpenseSubTypeLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<ExpenseSubTypeModel> getExpenseSubTypeSubModel()
        {
            List<ExpenseSubTypeModel> CSM = (from c in _context.ER_ExpenseSubType.Where(q => q.IsActive == true)
                                             join p in _context.ER_ExpenseType.Where(e => e.IsActive == true)
                                                 on c.ExpenseType_UUID equals p.UUID
                                             select new ExpenseSubTypeModel
                                             {
                                                 CM = p,
                                                 CTY = c,
                                                 UUID = c.UUID,
                                                 ExpenseType_Name = p.ExpenseType_Name,
                                                 ExpenseSubType_Name = c.ExpenseSubType_Name,
                                             }).ToList();
            return CSM;
        }
    }
}
