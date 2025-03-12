using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_EnvironmentRepository : Repository<Master_Environment>
    {
        private AppDbContext _context;
        public Master_EnvironmentRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Environment Update(Master_Environment obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
