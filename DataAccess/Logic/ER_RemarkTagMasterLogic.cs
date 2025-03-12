using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class ER_RemarkTagMasterLogic : ER_RemarkTagMasterRepository
    {
        private AppDbContext _context;
        public ER_RemarkTagMasterLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
