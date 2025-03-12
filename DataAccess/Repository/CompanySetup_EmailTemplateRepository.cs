using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CompanySetup_EmailTemplateRepository : Repository<CompanySetup_EmailTemplate>
    {
        private AppDbContext _context;
        public CompanySetup_EmailTemplateRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override CompanySetup_EmailTemplate Update(CompanySetup_EmailTemplate obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
