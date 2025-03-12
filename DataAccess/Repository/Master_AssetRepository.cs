using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_AssetRepository : Repository<Master_Asset>
    {
        private AppDbContext _context;
        public Master_AssetRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Asset Update(Master_Asset obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
