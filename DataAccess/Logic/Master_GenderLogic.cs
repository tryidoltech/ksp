using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Master_GenderLogic : Master_GenderRepository
    {
        private AppDbContext _context;
        public Master_GenderLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
