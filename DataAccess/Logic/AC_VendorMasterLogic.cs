using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class AC_VendorMasterLogic : AC_VendorMasterRepository
    {
        private AppDbContext _context;
        public AC_VendorMasterLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
