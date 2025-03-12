using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRA_ProfessionalTaxRepository : Repository<HRA_ProfessionalTax>
    {
        private AppDbContext _context;
        public HRA_ProfessionalTaxRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRA_ProfessionalTax Update(HRA_ProfessionalTax obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
