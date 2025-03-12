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
    public class AC_NomenClatureLogic : AC_NomenClatureRepository
    {
        private AppDbContext _context;
        public AC_NomenClatureLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<NomenClatureModel> getNomenClatureSubModel()
        {
            List<NomenClatureModel> CSM = (from c in _context.AC_NomenClature.Where(q => q.IsActive == true)
                                           join p in _context.AC_FinancialYear.Where(s => s.IsActive == true)
                                               on c.FinancialYear_UUID equals p.UUID
                                           select new NomenClatureModel
                                           {
                                               CM = p,
                                               CTY = c,
                                               UUID = c.UUID,
                                               Title = p.Title
                                           }).ToList();


            return CSM;
        }
    }
}
