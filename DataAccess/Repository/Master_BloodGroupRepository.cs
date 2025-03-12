using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_BloodGroupRepository : Repository<Master_BloodGroup>
    {
        private AppDbContext _context;
        public Master_BloodGroupRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_BloodGroup Update(Master_BloodGroup obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
