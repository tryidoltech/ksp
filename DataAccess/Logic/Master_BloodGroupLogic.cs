using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Master_BloodGroupLogic : Master_BloodGroupRepository
    {
        private AppDbContext _context;
        public Master_BloodGroupLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
