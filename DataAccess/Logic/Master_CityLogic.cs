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
    public class Master_CityLogic : Master_CityRepository
    {
        private AppDbContext _context;
        public Master_CityLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
        public List<CityModel> getCityMasterSubModel()
        {
            List<CityModel> CSM = (from c in _context.Master_City.Where(q => q.IsActive == true)
                                   join s in _context.Master_State.Where(s => s.IsActive == true)
                                       on c.State_UUID equals s.UUID
                                   join p in _context.Master_Country.Where(q => q.IsActive == true)
                                       on s.Country_UUID equals p.UUID
                                   select new CityModel
                                   {
                                       CM = p,
                                       SM = s,
                                       CTY = c, 
                                       UUID = c.UUID, 
                                       CityName = c.City_Name, 
                                       CountryName = p.CountryName,
                                       StateName = s.State_Name
                                   }).ToList();

            return CSM;


        }
    }
}
