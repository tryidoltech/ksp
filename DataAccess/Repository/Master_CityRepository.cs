using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_CityRepository : Repository<Master_City>
    {
        private AppDbContext _context;
        public Master_CityRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_City Update(Master_City obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
