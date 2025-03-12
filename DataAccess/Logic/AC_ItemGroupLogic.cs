using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class AC_ItemGroupLogic : AC_ItemGroupRepository
    {
        private AppDbContext _context;
        public AC_ItemGroupLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
