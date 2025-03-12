using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_UserMenuRightRepository : Repository<Master_User_MenuRight>
    {
        private AppDbContext _context;
        public Master_UserMenuRightRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_User_MenuRight Update(Master_User_MenuRight obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
