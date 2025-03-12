using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_DesignationRepository : Repository<Master_Designation>
    {
        private AppDbContext _context;
        public Master_DesignationRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Designation Update(Master_Designation obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
