using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_NationalityRepository : Repository<Master_Nationality>
    {
        private AppDbContext _context;
        public Master_NationalityRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Nationality Update(Master_Nationality obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
