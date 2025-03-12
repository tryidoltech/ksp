using Live_ConsultingKSP.Models;
using Live_ConsultingKSP.Services;
using Microsoft.EntityFrameworkCore;

namespace Live_ConsultingKSP.Services
{
    public class ExpenseDateRangeService : IExpenseDateRangeService
    {
        private readonly KsperpDbContext _context;

        public ExpenseDateRangeService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddExpenseDateRange(ErExpenseDataRange range)
        {
            if (_context.ErExpenseDataRanges.Any(r => r.Days == range.Days))
                throw new Exception("Expense Date Range already exists!");

            range.Uuid = Guid.NewGuid().ToString();
            range.IsActive = true;
            range.IsAddedOn = DateTime.Now;
            range.IsAddedBy = "1";

            _context.ErExpenseDataRanges.Add(range);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<ErExpenseDataRange>> GetAllExpenseDateRanges(string cmpuuid, string envuuid)
        {
            return await _context.ErExpenseDataRanges
                .Where(r => (bool)r.IsActive && r.MasterCompanyUuid == cmpuuid && r.MasterEnvironmentUuid == envuuid)
                .OrderByDescending(r => r.ExpenseDataRangeId)
                .ToListAsync();
        }


        public ErExpenseDataRange GetExpenseDateRangeByUUID(Guid uuid)
        {
            return _context.ErExpenseDataRanges.FirstOrDefault(r => r.Uuid == uuid.ToString());
        }

        public void UpdateExpenseDateRange(ErExpenseDataRange range)
        {
            var existingRange = _context.ErExpenseDataRanges.FirstOrDefault(r => r.Uuid == range.Uuid);
            if (existingRange == null) throw new Exception("Expense Date Range not found!");

            if (_context.ErExpenseDataRanges.Any(r => r.Days == range.Days && r.Uuid != range.Uuid))
                throw new Exception("Expense Date Range with the same day already exists!");

            existingRange.Days = range.Days;
            existingRange.IsDisplay = range.IsDisplay;
            existingRange.IsUpdatedOn = DateTime.Now;
            existingRange.IsUpdatedBy = "1";

            _context.SaveChanges();
        }

        public void DeleteExpenseDateRange(Guid uuid)
        {
            var range = _context.ErExpenseDataRanges.FirstOrDefault(r => r.Uuid == uuid.ToString());
            if (range == null) throw new Exception("Expense Date Range not found!");

            range.IsActive = false;
            range.IsDeletedOn = DateTime.Now;
            range.IsDeletedBy = "1";

            _context.SaveChanges();
        }
    }
}