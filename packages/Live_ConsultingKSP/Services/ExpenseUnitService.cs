using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public class ExpenseUnitService : IExpenseUnitService
    {
        private readonly KsperpDbContext _context;

        public ExpenseUnitService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddExpenseUnit(ErExpenseUnit unit)
        {
            if (_context.ErExpenseUnits.Any(u => u.UnitName == unit.UnitName))
                throw new Exception("Expense Unit already exists!");

            unit.Uuid = Guid.NewGuid().ToString();
            unit.IsActive = true;
            unit.IsAddedOn = DateTime.Now;
            unit.IsAddedBy = "1";

            _context.ErExpenseUnits.Add(unit);
            _context.SaveChanges();
        }

        public List<ErExpenseUnit> GetAllExpenseUnits()
        {
            return _context.ErExpenseUnits
                .Where(u => u.IsActive == true)
                .OrderByDescending(u => u.ExpenseUnitId)
                .ToList();
        }

        public ErExpenseUnit GetExpenseUnitByUUID(Guid uuid)
        {
            return _context.ErExpenseUnits.FirstOrDefault(u => u.Uuid == uuid.ToString());
        }

        public void UpdateExpenseUnit(ErExpenseUnit unit)
        {
            var existingUnit = _context.ErExpenseUnits.FirstOrDefault(u => u.Uuid == unit.Uuid);
            if (existingUnit == null) throw new Exception("Expense Unit not found!");

            if (_context.ErExpenseUnits.Any(u => u.UnitName == unit.UnitName && u.Uuid != unit.Uuid))
                throw new Exception("Expense Unit with the same name already exists!");

            existingUnit.UnitName = unit.UnitName;
            existingUnit.IsDisplay = unit.IsDisplay;
            existingUnit.IsUpdatedOn = DateTime.Now;
            existingUnit.IsUpdatedBy = "1";

            _context.SaveChanges();
        }

        public void DeleteExpenseUnit(Guid uuid)
        {
            var unit = _context.ErExpenseUnits.FirstOrDefault(u => u.Uuid == uuid.ToString());
            if (unit == null) throw new Exception("Expense Unit not found!");

            unit.IsActive = false;
            unit.IsDeletedOn = DateTime.Now;
            unit.IsDeletedBy = "1";

            _context.SaveChanges();
        }
    }
}
