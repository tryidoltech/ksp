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
    public class Master_EmployeeLogic : Master_EmployeeRepository
    {
        private AppDbContext _context;
        public Master_EmployeeLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
        public List<EmployeeModel> GetEmployeeDetails()
        {
            List<EmployeeModel> employees = (from emp in _context.Master_Employee.Where(e => e.IsActive == true)
                                             join h in _context.Master_Honorific.Where(h => h.IsActive == true)
                                                 on emp.Master_Prefix_UUID equals h.Uuid
                                             join b in _context.Master_BloodGroup.Where(b => b.IsActive == true)
                                                 on emp.Master_BloodGroup_UUID equals b.Uuid
                                             join d in _context.Master_Department.Where(d => d.IsActive == true)
                                                 on emp.Master_Department_UUID equals d.UUID
                                             join g in _context.Master_Gender.Where(g => g.IsActive == true)
                                                 on emp.Master_Gender_UUID equals g.Uuid
                                             join c in _context.Master_Country.Where(c => c.IsActive == true)
                                                 on emp.Master_Country_UUID equals c.UUID
                                             join s in _context.Master_State.Where(s => s.IsActive == true)
                                                 on emp.Master_State_UUID equals s.UUID
                                             join ct in _context.Master_City.Where(ct => ct.IsActive == true)
                                                 on emp.Master_City_UUID equals ct.UUID
                                             select new EmployeeModel
                                             {
                                                 EM = emp,
                                                 HM = h,
                                                 BM = b,
                                                 DM = d,
                                                 GM = g,
                                                 CM = c,
                                                 SM = s,
                                                 CTY = ct,
                                                 UUID = emp.UUID,
                                                 PrefixName = h.Honorific_name,
                                                 BloodGroupName = b.BloodGroup_name,
                                                 DepartmentName = d.Department_Name,
                                                 GenderName = g.Gender_name,
                                                 CountryName = c.CountryName,
                                                 StateName = s.State_Name,
                                                 CityName = ct.City_Name
                                             }).ToList();

            return employees;
        }
    }
}
