using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class HRAIncome_IncomeTaxLogic : HRAIncome_IncomeTaxRepository
    {
        private AppDbContext _context;
        public HRAIncome_IncomeTaxLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
