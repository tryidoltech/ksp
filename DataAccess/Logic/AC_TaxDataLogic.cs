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
    public class AC_TaxDataLogic : AC_TaxDataRepository
    {
        private AppDbContext _context;
        public AC_TaxDataLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<TaxDataModel> getTaxDataSubModel()
        {
            List<TaxDataModel> CSM = (from c in _context.AC_TaxData.Where(q => q.IsActive == true)
                                      join p in _context.AC_TaxGroup.Where(s => s.IsActive == true)
                                          on c.TaxGroup_UUID equals p.UUID
                                      join f in _context.AC_Taxcode.Where(s => s.IsActive == true)
                                      on c.TaxCode_UUID equals f.UUID
                                      select new TaxDataModel
                                      {
                                          CM = p,
                                          SM = f,
                                          CTY = c,
                                          //UUID = c.UUID,
                                          TaxGroup_Name = p.TaxGroup_Name,
                                          Tax_Name = f.Tax_Name,
                                      }).ToList();


            return CSM;
        }

    }
}
