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
    public class ER_RemarkTemplateLogic : ER_RemarkTemplateRepository
    {
        private AppDbContext _context;
        public ER_RemarkTemplateLogic(AppDbContext context)
            : base(context)
        {
            _context = context;
        }


        public List<ErRemarkTemplateModel> GetRemarkTempSubModel()
        {
            var ERT = (from mt in _context.ER_RemarkTemplate.Where(q => q.IsActive == true)
                       join pt in _context.ER_RemarkTagMaster.Where(q => q.IsActive == true)
                       on mt.RemarkTag_UUID equals pt.UUID
                       join pp in _context.ER_ExpenseType.Where(q => q.IsActive == true)
                       on mt.ExpenseType_UUID equals pp.UUID
                       join cp in _context.ER_ExpenseSubType.Where(q => q.IsActive == true)
                       on mt.ExpenseSubType_UUID equals cp.UUID
                       select new ErRemarkTemplateModel
                       {
                           UUID = mt.UUID,
                           ExpenseType_Name = pp.ExpenseType_Name,
                           ExpenseSubType_Name = cp.ExpenseSubType_Name,
                           RemarkTag_Name = pt.RemarkTag_Name,
                           Remark_String = mt.Remark_String,
                           MT = mt,
                           PT = pt,
                           PP = cp,
                           CP = pp
                       }).ToList();

            return ERT;
        }
    }
}
    
