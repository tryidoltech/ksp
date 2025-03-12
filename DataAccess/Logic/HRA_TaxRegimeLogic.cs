using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class HRA_TaxRegimeLogic : HRA_TaxRegimeRepository
    {
        private AppDbContext _context;
        public HRA_TaxRegimeLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
