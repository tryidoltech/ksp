using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class HRA_ManageLeaveLogic : HRA_ManageLeaveRepository
    {
        private AppDbContext _context;
        public HRA_ManageLeaveLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
