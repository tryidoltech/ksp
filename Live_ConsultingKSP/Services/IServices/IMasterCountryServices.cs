using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IMasterCountryServices
    {
        void AddCountry(MasterCountry country);
        Task<IEnumerable<MasterCountry>> GetAllCountries(string cmpuuid, string envuuid);
        MasterCountry GetCountryByUUID(Guid uuid);
        void UpdateCountry(MasterCountry country);
        void DeleteCountry(Guid uuid);
    }
}
