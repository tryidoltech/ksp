using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_ServiceRepository : Repository<Master_Service>
    {
        private AppDbContext _context;
        public Master_ServiceRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Service Update(Master_Service obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
