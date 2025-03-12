using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IHraShiftService
    {
        void AddShift(HraShift shift);
        List<HraShift> GetAllShifts();
        HraShift GetShiftByUUID(string uuid);
        void UpdateShift(HraShift shift);
        void DeleteShift(string uuid);
    }
}
