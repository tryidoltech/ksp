using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_CompanyBranchRepository : Repository<Master_CompanyBranch>
    {
        private AppDbContext _context;
        public Master_CompanyBranchRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_CompanyBranch Update(Master_CompanyBranch obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
