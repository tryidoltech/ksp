using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class AC_CustomerMasterLogic : AC_CustomerMasterRepository
    {
        private AppDbContext _context;
        public AC_CustomerMasterLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
