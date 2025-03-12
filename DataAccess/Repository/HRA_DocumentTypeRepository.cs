using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HRA_DocumentTypeRepository : Repository<HRA_DocumentType>
    {
        private AppDbContext _context;
        public HRA_DocumentTypeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override HRA_DocumentType Update(HRA_DocumentType obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
