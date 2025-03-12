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
    public class HRAEmployee_WeekOffLogic : HRAEmployee_WeekOffRepository
    {
        private AppDbContext _context;
        public HRAEmployee_WeekOffLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
        public List<HRAEmployeeWeekOffModel> GetHRAEmployeeWeekoff()
        {
            

                var WEEK = (from woff in _context.HRAEmployee_WeekOff.Where(e => e.IsActive == true)
                                      join branch in _context.Master_CompanyBranch.Where(cn => cn.IsActive == true)
                                      on woff.MasterCompanyBranch_UUID equals branch.UUID

                                      join emp in _context.Master_Employee.Where(c => c.IsActive == true)
                                      on woff.Employee_UUID equals emp.UUID

                                      join day in _context.HRA_WeekDay.Where(s => s.IsActive == true)
                                      on woff.WeekDay_UUID equals day.UUID

                                      select new HRAEmployeeWeekOffModel
                                      {
                                          OWN = woff,
                                          ME = emp,
                                          WD = day,
                                          CB = branch,
                                          UUID = woff.UUID,
                                          FirstName = emp.FirstName + " " + emp.LastName,
                                          Branch_Name = branch.Branch_Name,
                                          WeekDay_Name = day.WeekDay_Name
                                      }).ToList();

            return WEEK;
           
        }

    }
}
