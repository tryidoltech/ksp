using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Project_ProjectStatusLogic : Project_ProjectStatusRepository
    {
        private AppDbContext _context;
        public Project_ProjectStatusLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
