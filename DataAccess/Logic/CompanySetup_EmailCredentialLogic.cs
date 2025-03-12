using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class CompanySetup_EmailCredentialLogic : CompanySetup_EmailCredentialRepository
    {
        private AppDbContext _context;
        public CompanySetup_EmailCredentialLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
