using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Master_Employee_EducationDetailsRepository : Repository<Master_Employee_EducationDetails>
    {
        private AppDbContext _context;
        public Master_Employee_EducationDetailsRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public override Master_Employee_EducationDetails Update(Master_Employee_EducationDetails obj)
        {
            // obj.UpdatedOn = ServerDate.Now();
            return base.Update(obj);
        }
    }
}