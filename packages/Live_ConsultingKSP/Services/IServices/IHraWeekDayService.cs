using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IHraWeekDayService
    {
        void AddWeekDay(HraWeekDay weekDay);
        List<HraWeekDay> GetAllWeekDays();
        IQueryable<HraWeekDay> GetAllWeekDaysQueryable();
        HraWeekDay GetWeekDayByUUID(string uuid);
        void UpdateWeekDay(HraWeekDay weekDay);
        void DeleteWeekDay(string uuid);
    }
}
