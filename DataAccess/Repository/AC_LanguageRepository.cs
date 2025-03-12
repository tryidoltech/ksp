using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AC_LanguageRepository : Repository<AC_Language>
    {
        private AppDbContext _context;
        public AC_LanguageRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override AC_Language Update(AC_Language obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
