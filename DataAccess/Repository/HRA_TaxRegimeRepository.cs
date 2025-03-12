using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRA_TaxRegimeRepository : Repository<HRA_TaxRegime>
    {
        private AppDbContext _context;
        public HRA_TaxRegimeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRA_TaxRegime Update(HRA_TaxRegime obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
