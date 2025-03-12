using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Master_BanktypeLogic : Master_BanktypeRepository
    {
        private AppDbContext _context;
        public Master_BanktypeLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
