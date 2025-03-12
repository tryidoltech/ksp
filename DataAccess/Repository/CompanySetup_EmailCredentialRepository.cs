using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CompanySetup_EmailCredentialRepository : Repository<CompanySetup_EmailCredential>
    {
        private AppDbContext _context;
        public CompanySetup_EmailCredentialRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override CompanySetup_EmailCredential Update(CompanySetup_EmailCredential obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
