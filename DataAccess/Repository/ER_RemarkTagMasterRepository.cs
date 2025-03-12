using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ER_RemarkTagMasterRepository : Repository<ER_RemarkTagMaster>
    {
        private AppDbContext _context;
        public ER_RemarkTagMasterRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ER_RemarkTagMaster Update(ER_RemarkTagMaster obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
