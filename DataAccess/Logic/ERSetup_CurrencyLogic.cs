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
    public class ERSetup_CurrencyLogic : ERSetup_CurrencyRepository
    {
        private AppDbContext _context;
        public ERSetup_CurrencyLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
        public List<ERCurrencyModel> GetCurrency()
        {
            List<ERCurrencyModel> SSM = (from p in _context.ERSetup_Currency.Where(q => q.IsActive == true)
                                         join r in _context.Master_Country.Where(t => t.IsActive == true)
                                         on p.Country_UUID equals r.UUID
                                         select new ERCurrencyModel
                                         {
                                             EC = p,
                                             CM = r,
                                             CountryName = r.CountryName
                                         }).ToList();
            return SSM;
        }
    }
}
