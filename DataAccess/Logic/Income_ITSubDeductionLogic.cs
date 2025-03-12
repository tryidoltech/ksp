using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Income_ITSubDeductionLogic : Income_ITSubDeductionRepository
    {
        private AppDbContext _context;
        public Income_ITSubDeductionLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
