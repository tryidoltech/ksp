using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Project_ProjectDocumentRepository : Repository<Project_ProjectDocument>
    {
        private AppDbContext _context;
        public Project_ProjectDocumentRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Project_ProjectDocument Update(Project_ProjectDocument obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}
