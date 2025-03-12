using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_ItemMasterRepository : Repository<AC_ItemMaster>
    {
        private AppDbContext _context;
        public AC_ItemMasterRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_ItemMaster Update(AC_ItemMaster obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
