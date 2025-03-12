using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_CategoryRepository : Repository<Master_Category>
    {
        private AppDbContext _context;
        public Master_CategoryRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Category Update(Master_Category obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
