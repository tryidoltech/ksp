using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_ItemGroupRepository : Repository<AC_ItemGroup>
    {
        private AppDbContext _context;
        public AC_ItemGroupRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_ItemGroup Update(AC_ItemGroup obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
