using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_UserRoleRepository : Repository<Master_User_Role>
    {
        private AppDbContext _context;
        public Master_UserRoleRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_User_Role Update(Master_User_Role obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
