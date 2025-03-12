using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IHraTaxRegimeService
    {
        void AddTaxRegime(HraTaxRegime taxRegime);
        List<HraTaxRegime> GetAllTaxRegimes();
        HraTaxRegime GetTaxRegimeByUUID(string uuid);
        void UpdateTaxRegime(HraTaxRegime taxRegime);
        void DeleteTaxRegime(string uuid);
    }
}
