using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IExpenseUnitService
    {
        void AddExpenseUnit(ErExpenseUnit unit);
        List<ErExpenseUnit> GetAllExpenseUnits();
        ErExpenseUnit GetExpenseUnitByUUID(Guid uuid);
        void UpdateExpenseUnit(ErExpenseUnit unit);
        void DeleteExpenseUnit(Guid uuid);
    }
}
