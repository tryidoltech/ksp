using DataAccess.Entities;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Logic
{
    public class Master_Employee_AssestsDetailsLogic : Master_Employee_AssestsDetailsRepository
    {
        private AppDbContext _context;
        public Master_Employee_AssestsDetailsLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}