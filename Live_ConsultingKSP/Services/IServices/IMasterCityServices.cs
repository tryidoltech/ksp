using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IMasterCityServices
    {
        IEnumerable<MasterCity> GetAllCities(string cmpuuid, string envuuid);
        void AddCity(MasterCity city);
       
        MasterCity GetCitiesByUUID(Guid uuid);
        void UpdateCity(MasterCity city);
        void DeleteCities(Guid uuid);

    }
}
