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
    public class AC_ITSubdeductionLogic : AC_ITSubdeductionRepository
    {
        private AppDbContext _context;
        public AC_ITSubdeductionLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<ACSubITDeductionModel> getsubITdeductionSubModel()
        {
            List<ACSubITDeductionModel> CSM = (from c in _context.AC_ITSubdeduction.Where(q => q.IsActive == true)
                                      join p in _context.AC_ITDeduction.Where(s => s.IsActive == true)
                                          on c.ITDeduction_UUID equals p.UUID
                                      
                                      select new ACSubITDeductionModel
                                      {
                                          CM = p,
                                          CTY = c,
                                          //UUID = c.UUID,
                                          ITDeduction_Name = p.ITDeduction_Name,
                                      }).ToList();


            return CSM;
        }

    }
}
