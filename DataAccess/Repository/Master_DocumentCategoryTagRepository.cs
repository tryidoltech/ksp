using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_DocumentCategoryTagRepository : Repository<Master_DocumentCategoryTag>
    {
        private AppDbContext _context;
        public Master_DocumentCategoryTagRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_DocumentCategoryTag Update(Master_DocumentCategoryTag obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
