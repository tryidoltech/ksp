using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BD_ReasearchCommunicationStatusRepository : Repository<BD_ReasearchCommunicationStatus>
    {
        private AppDbContext _context;
        public BD_ReasearchCommunicationStatusRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override BD_ReasearchCommunicationStatus Update(BD_ReasearchCommunicationStatus obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
