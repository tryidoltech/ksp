using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BD_CommunicationHistoryRepository : Repository<BD_CommunicationHistory>
    {
        private AppDbContext _context;
        public BD_CommunicationHistoryRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override BD_CommunicationHistory Update(BD_CommunicationHistory obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
