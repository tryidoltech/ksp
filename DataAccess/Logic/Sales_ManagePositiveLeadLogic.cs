using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Sales_ManagePositiveLeadLogic : Sales_ManagePositiveLeadRepository
    {
        private AppDbContext _context;
        public Sales_ManagePositiveLeadLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
