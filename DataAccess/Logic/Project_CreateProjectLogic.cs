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
    public class Project_CreateProjectLogic : Project_CreateProjectRepository
    {
        private AppDbContext _context;
        public Project_CreateProjectLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<CreateProjectModel> getCreateprojectSubModel()
        {
            List<CreateProjectModel> CSM = (from c in _context.Project_CreateProject.Where(q => q.IsActive == true)
                                            join s in _context.AC_CustomerMaster.Where(s => s.IsActive == true)
                                                on c.Customer_UUID equals s.UUID
                                            join p in _context.Master_Employee.Where(e => e.IsActive == true)
                                                on c.Employee_UUID equals p.UUID
                                            select new CreateProjectModel
                                            {
                                                CM = s,
                                                SM = p,
                                                CTY = c,

                                                CustomerMaster_Name = s.CustomerMaster_Name,
                                                FirstName = p.FirstName + " " + p.LastName,
                                            }).ToList();
            return CSM;
        }

    }
}
