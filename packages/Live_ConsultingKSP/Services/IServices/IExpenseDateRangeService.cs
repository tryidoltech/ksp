using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IExpenseDateRangeService
    {
        void AddExpenseDateRange(ErExpenseDataRange range);
        Task<IEnumerable<ErExpenseDataRange>>GetAllExpenseDateRanges(string cmpuuid, string envuuid);
        ErExpenseDataRange GetExpenseDateRangeByUUID(Guid uuid);
        void UpdateExpenseDateRange(ErExpenseDataRange range);
        void DeleteExpenseDateRange(Guid uuid);
    }
}
