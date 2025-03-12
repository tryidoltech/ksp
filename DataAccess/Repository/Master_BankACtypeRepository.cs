using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_BankACtypeRepository : Repository<Master_BankACtype>
    {
        private AppDbContext _context;
        public Master_BankACtypeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_BankACtype Update(Master_BankACtype obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
