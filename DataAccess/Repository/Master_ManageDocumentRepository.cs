using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_ManageDocumentRepository : Repository<Master_ManageDocument>
    {
        private AppDbContext _context;
        public Master_ManageDocumentRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_ManageDocument Update(Master_ManageDocument obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
