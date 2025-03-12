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
    public class AC_IncomeTaxMasterLogic : AC_IncomeTaxMasterRepository
    {
        private AppDbContext _context;
        public AC_IncomeTaxMasterLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<IncomeTaxMasterModel> getIncomeTaxMasterSubModel()
        {
            List<IncomeTaxMasterModel> CSM = (from c in _context.AC_IncomeTaxMaster.Where(q => q.IsActive == true)
                                      join p in _context.HRA_TaxRegime.Where(s => s.IsActive == true)
                                          on c.Regime_UUID equals p.UUID
                                      join f in _context.Master_Gender.Where(s => s.IsActive == true)
                                      on c.Gender_UUID equals f.Uuid
                                      select new IncomeTaxMasterModel
                                      {
                                          CM = p,
                                          SM = f,
                                          CTY = c,
                                          //UUID = c.UUID,
                                          Gender_name = f.Gender_name,
                                          Tax_Regime = p.Tax_Regime,
                                      }).ToList();


            return CSM;
        }

    }
}
