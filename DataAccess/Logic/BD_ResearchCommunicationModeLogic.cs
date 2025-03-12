using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class BD_ResearchCommunicationModeLogic : BD_ResearchCommunicationModeRepository
    {
        private AppDbContext _context;
        public BD_ResearchCommunicationModeLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
