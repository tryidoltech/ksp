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
    public class ER_SubstituteExpenseLogic : ER_SubstituteExpenseRepository
    {
        private AppDbContext _context;
        public ER_SubstituteExpenseLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }


        public List<ERSubstituteExpenseModel> getErSubstituteSubModel()
        {
            List<ERSubstituteExpenseModel> CSM = (from c in _context.ER_SubstituteExpense.Where(q => q.IsActive == true)
                                                  join s in _context.ER_ExpenseType.Where(s => s.IsActive == true)
                                                      on c.ExpenseTypePrimary_UUID equals s.UUID
                                                  join p in _context.ER_ExpenseType.Where(e => e.IsActive == true)
                                                      on c.ExpenseTypeSecondary_UUID equals p.UUID
                                                  select new ERSubstituteExpenseModel
                                                  {
                                                      CM = p,
                                                      CTY = c,
                                                      UUID = c.UUID,
                                                      Primary_ExpenseType_Name = s.ExpenseType_Name,
                                                      Secoundary_ExpenseType_Name = p.ExpenseType_Name


                                                  }).ToList();
            return CSM;
        }
    }
}