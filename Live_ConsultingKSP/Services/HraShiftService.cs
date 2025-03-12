using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public class HraShiftService : IHraShiftService
    {
        private readonly KsperpDbContext _context;

        public HraShiftService(KsperpDbContext context)
        {
            _context = context;
        }

        public void AddShift(HraShift shift)
        {
            if (_context.HraShifts.Any(sh => sh.ShiftName == shift.ShiftName))
                throw new Exception("Shift already exists!");

            shift.IsActive = true;
            shift.IsAddedOn = DateTime.Now;
            shift.IsAddedBy = "1";

            _context.HraShifts.Add(shift);
            _context.SaveChanges();
        }

        public List<HraShift> GetAllShifts()
        {
            return _context.HraShifts
                .Where(sh => sh.IsActive == true)
                .OrderByDescending(sh => sh.ShiftId)
                .ToList();
        }

        public HraShift GetShiftByUUID(string uuid)
        {
            return _context.HraShifts.FirstOrDefault(sh => sh.Uuid == uuid);
        }

        public void UpdateShift(HraShift shift)
        {
            var existingShift = _context.HraShifts.FirstOrDefault(sh => sh.Uuid == shift.Uuid);
            if (existingShift == null) throw new Exception("Shift not found!");

            if (_context.HraShifts.Any(sh => sh.ShiftName == shift.ShiftName && sh.Uuid != shift.Uuid))
                throw new Exception("Shift with the same name already exists!");

            existingShift.ShiftName = shift.ShiftName;
            existingShift.ShiftPrefix = shift.ShiftPrefix;
            existingShift.StartTime = shift.StartTime;
            existingShift.EndTime = shift.EndTime;
            existingShift.LunchTime = shift.LunchTime;
            existingShift.TotalWorkingHours = shift.TotalWorkingHours;
            existingShift.IsDisplay = shift.IsDisplay;
            existingShift.IsUpdatedOn = DateTime.Now;
            existingShift.IsUpdatedBy = "1";

            _context.SaveChanges();
        }

        public void DeleteShift(string uuid)
        {
            var shift = _context.HraShifts.FirstOrDefault(sh => sh.Uuid == uuid);
            if (shift == null) throw new Exception("Shift not found!");

            shift.IsActive = false;
            shift.IsDeletedOn = DateTime.Now;
            shift.IsDeletedBy = "1";

            _context.SaveChanges();
        }
    }
}
