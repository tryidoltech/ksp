using DataAccess.Entities;
using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Project_ProjectDocumentLogic : Project_ProjectDocumentRepository
    {
        private AppDbContext _context;
        public Project_ProjectDocumentLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }


        public List<ProjectDocumentModel> getProjectDocumentSubModel()
        {
            List<ProjectDocumentModel> CSM = (from c in _context.Project_ProjectDocument.Where(q => q.IsActive == true)
                                              join p in _context.Project_CreateProject.Where(s => s.IsActive == true)
                                                  on c.Project_UUID equals p.UUID
                                              select new ProjectDocumentModel
                                              {
                                                  PC = p,
                                                  PD = c,
                                                  UUID = c.UUID,
                                                  Title = c.Title,
                                                  Project_Title = p.Project_Title
                                              }).ToList();


            return CSM;
        }


    }
}

