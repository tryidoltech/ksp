using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_CountryRepository : Repository<Master_Country>
    {
        private AppDbContext _context;
        public Master_CountryRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Country Update(Master_Country obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
