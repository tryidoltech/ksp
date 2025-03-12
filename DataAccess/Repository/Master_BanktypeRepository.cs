using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_BanktypeRepository : Repository<Master_Banktype>
    {
        private AppDbContext _context;
        public Master_BanktypeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Banktype Update(Master_Banktype obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
