using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ER_RemarkTemplateRepository : Repository<ER_RemarkTemplate>
    {
        private AppDbContext _context;
        public ER_RemarkTemplateRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override ER_RemarkTemplate Update(ER_RemarkTemplate obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
