using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Research_ManageFilterDataRepository : Repository<Research_ManageFilterData>
    {
        private AppDbContext _context;
        public Research_ManageFilterDataRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Research_ManageFilterData Update(Research_ManageFilterData obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
