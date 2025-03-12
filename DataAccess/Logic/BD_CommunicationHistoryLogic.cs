using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class BD_CommunicationHistoryLogic : BD_CommunicationHistoryRepository
    {
        private AppDbContext _context;
        public BD_CommunicationHistoryLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
