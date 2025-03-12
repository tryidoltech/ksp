using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_DocumentGropRepository : Repository<Master_DocumentGrop>
    {
        private AppDbContext _context;
        public Master_DocumentGropRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_DocumentGrop Update(Master_DocumentGrop obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
