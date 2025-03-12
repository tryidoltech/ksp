using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_CategorTagRepository : Repository<Master_CategorTag>
    {
        private AppDbContext _context;
        public Master_CategorTagRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_CategorTag Update(Master_CategorTag obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
