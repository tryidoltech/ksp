using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Research_UploadResearchDataLogic : Research_UploadResearchDataRepository
    {
        private AppDbContext _context;
        public Research_UploadResearchDataLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
