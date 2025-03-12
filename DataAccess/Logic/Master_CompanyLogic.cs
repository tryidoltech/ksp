using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Master_CompanyLogic : Master_CompanyRepository
    {
        private AppDbContext _context;
        public Master_CompanyLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
