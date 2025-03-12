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
    public class AC_TaxcodeLogic : AC_TaxcodeRepository
    {
        private AppDbContext _context;
        public AC_TaxcodeLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<TaxCodeModel> getTaxCodeSubModel()
        {
            List<TaxCodeModel> CSM = (from c in _context.AC_Taxcode.Where(q => q.IsActive == true)
                                      join p in _context.AC_TaxGroup.Where(s => s.IsActive == true)
                                          on c.TaxGroup_UUID equals p.UUID
                                      select new TaxCodeModel
                                      {
                                          CM = p,
                                          CTY = c,
                                          //UUID = c.UUID,
                                          TaxGroup_Name = p.TaxGroup_Name,
                                      }).ToList();


            return CSM;
        }

    }
}
