using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_MaritalRepository : Repository<Master_Marital>
    {
        private AppDbContext _context;
        public Master_MaritalRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Marital Update(Master_Marital obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
