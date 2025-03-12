using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Master_BankACtypeLogic : Master_BankACtypeRepository
    {
        private AppDbContext _context;
        public Master_BankACtypeLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
