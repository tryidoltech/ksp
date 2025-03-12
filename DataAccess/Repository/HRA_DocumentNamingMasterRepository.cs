using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRA_DocumentNamingMasterRepository : Repository<HRA_DocumentNamingMaster>
    {
        private AppDbContext _context;
        public HRA_DocumentNamingMasterRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRA_DocumentNamingMaster Update(HRA_DocumentNamingMaster obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
