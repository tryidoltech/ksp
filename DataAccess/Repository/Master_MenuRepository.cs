using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_MenuRepository : Repository<Master_Menu>
    {
        private AppDbContext _context;
        public Master_MenuRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Menu Update(Master_Menu obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
