using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_HonorificRepository : Repository<Master_Honorific>
    {
        private AppDbContext _context;
        public Master_HonorificRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Honorific Update(Master_Honorific obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
