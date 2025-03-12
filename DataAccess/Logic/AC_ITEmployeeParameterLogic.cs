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
    public class AC_ITEmployeeParameterLogic : AC_ITEmployeeParameterRepository
    {
        private AppDbContext _context;
        public AC_ITEmployeeParameterLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<ITEmployeeParameterModel> getITEmployeeSubModel()
        {
            List<ITEmployeeParameterModel> CSM = (from c in _context.AC_ITEmployeeParameter.Where(q => q.IsActive == true)
                                                  join p in _context.AC_ITDeduction.Where(s => s.IsActive == true)
                                                      on c.ITDeduction_UUID equals p.UUID
                                                  join f in _context.Master_CompanyBranch.Where(s => s.IsActive == true)
                                                      on c.CompanyBranch_UUID equals f.UUID
                                                  join cb in _context.Master_Employee.Where(s => s.IsActive == true)
                                                      on c.Employee_UUID equals cb.UUID // Corrected join condition
                                                  join FY in _context.AC_FinancialYear.Where(s => s.IsActive == true)
                                                      on c.FinancialYear_UUID equals FY.UUID
                                                  select new ITEmployeeParameterModel
                                                  {
                                                      CM = p,
                                                      SM = f,
                                                      FY = FY,
                                                      ME = cb,
                                                      CTY = c,
                                                      ITDeduction_Name = p.ITDeduction_Name,
                                                      Title = FY.Title,
                                                      Branch_Name = f.Branch_Name,
                                                      FullName = cb.FirstName + " " + cb.LastName // Added space for readability
                                                  }).ToList();

            return CSM;


        }

    }
}
