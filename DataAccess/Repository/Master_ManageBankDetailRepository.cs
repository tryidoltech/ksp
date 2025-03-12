using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_ManageBankDetailRepository : Repository<Master_ManageBankDetail>
    {
        private AppDbContext _context;
        public Master_ManageBankDetailRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_ManageBankDetail Update(Master_ManageBankDetail obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
