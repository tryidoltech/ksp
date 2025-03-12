using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Master_HonorificLogic : Master_HonorificRepository
    {
        private AppDbContext _context;
        public Master_HonorificLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
