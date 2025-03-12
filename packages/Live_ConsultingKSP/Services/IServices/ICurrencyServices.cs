using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services.IServices
{
    public interface ICurrencyServices
    {
        void AddCurrency(ErsetupCurrency currency);
        List<ErsetupCurrency> GetAllCurrency(string cmpuuid, string envuuid);
        ErsetupCurrency GetByCurrency(Guid uuid);
        void UpdateCurrency(ErsetupCurrency currency);
        void DeleteCurrency(Guid uuid);
    }
}
