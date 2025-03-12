using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Research_ManageResearchGroupLogic : Research_ManageResearchGroupRepository
    {
        private AppDbContext _context;
        public Research_ManageResearchGroupLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
