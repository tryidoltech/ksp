using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services.IServices
{
    public interface IFinancialYearServices
    {
        void AddFinancialYear(AcFinancialYear acFinancialYear);
        List<AcFinancialYear> GetAllFinancialYear(string cmpuuid, string envuuid);
        AcFinancialYear GetByFinancialYear(Guid uuid);
        void UpdateFinancialYear(AcFinancialYear acFinancial);
        void DeleteFinancialYear(Guid uuid);
    }
}
