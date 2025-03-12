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
    public class Master_CompanyBranchLogic : Master_CompanyBranchRepository
    {
        private AppDbContext _context;
        public Master_CompanyBranchLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<CompanyBranchModel> GetCompanyBranchModel()
        {


            var companyBranch = (from companyB in _context.Master_CompanyBranch.Where(e => e.IsActive == true)
                                 join cn in _context.Master_Company.Where(cn => cn.IsActive == true)
                                 on companyB.Company_UUID equals cn.Uuid

                                 join c in _context.Master_Country.Where(c => c.IsActive == true)
                                 on companyB.Country_UUID equals c.UUID

                                 join s in _context.Master_State.Where(s => s.IsActive == true)
                                 on companyB.State_UUID equals s.UUID

                                 join ct in _context.Master_City.Where(ct => ct.IsActive == true)
                                 on companyB.City_UUID equals ct.UUID

                                 select new CompanyBranchModel
                                 {
                                     CB = companyB,
                                     CN = cn,
                                     CM = c,
                                     SM = s,
                                     CTY = ct,
                                     UUID = companyB.UUID,
                                     CompanyName = cn.CompanyName,
                                     CountryName = c.CountryName,
                                     StateName = s.State_Name,
                                     CityName = ct.City_Name,
                                     Branch_Name = companyB.Branch_Name,
                                     Branch_Address = companyB.Branch_Address,
                                 }).ToList();

            return companyBranch;
        }
    }

}






