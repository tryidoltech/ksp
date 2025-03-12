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
    public class Master_StateLogic : Master_StateRepository
    {
        private AppDbContext _context;
        public Master_StateLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
        public List<StateModel> getStateActiveSubModel()
        {
            List<StateModel> SSM = (from p in _context.Master_Country.Where(q => q.IsActive == true)
                                    join r in _context.Master_State.Where(t => t.IsActive == true)
                                    on p.UUID equals r.Country_UUID
                                    select new StateModel {
                                        CM = p,
                                        SM = r,
                                        CountryName = p.CountryName
                                    }).ToList();
            return SSM;
        }
    }
}
