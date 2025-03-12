using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_StateRepository : Repository<Master_State>
    {
        private AppDbContext _context;
        public Master_StateRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_State Update(Master_State obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
