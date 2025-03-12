using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class AC_LanguageLogic : AC_LanguageRepository
    {
        private AppDbContext _context;
        public AC_LanguageLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
