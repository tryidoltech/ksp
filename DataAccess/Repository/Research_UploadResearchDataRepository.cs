using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Research_UploadResearchDataRepository : Repository<Research_UploadResearchData>
    {
        private AppDbContext _context;
        public Research_UploadResearchDataRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Research_UploadResearchData Update(Research_UploadResearchData obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
