using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Master_MaritalLogic : Master_MaritalRepository
    {
        private AppDbContext _context;
        public Master_MaritalLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
