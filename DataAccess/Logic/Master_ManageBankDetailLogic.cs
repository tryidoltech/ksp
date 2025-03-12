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
    public class Master_ManageBankDetailLogic : Master_ManageBankDetailRepository
    {
        private AppDbContext _context;
        public Master_ManageBankDetailLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public List<ManageBankDetailsModel> getManageBankSubModel()
        {
            List<ManageBankDetailsModel> CSM = (from c in _context.Master_ManageBankDetail.Where(q => q.IsActive == true)
                                      join p in _context.Master_Employee.Where(s => s.IsActive == true)
                                          on c.Employee_UUID equals p.UUID
                                      join bt in _context.Master_Banktype.Where(s => s.IsActive == true)
                                          on c.Bank_UUID equals bt.Uuid
                                      join bact in _context.Master_BankACtype.Where(s => s.IsActive == true)
                                          on c.Account_UUID equals bact.Uuid
                                                select new ManageBankDetailsModel
                                      {
                                          CM = p,
                                          SM = bt,
                                          BM = bact,
                                          CTY = c,
                                          //UUID = c.UUID,
                                           FullName = p.FirstName + " " + p.LastName,
                                           Bank_name = bt.Bank_name,
                                           Bank_AccontType = bact.Bank_AccontType,
                                      }).ToList();


            return CSM;
        }

    }
}
