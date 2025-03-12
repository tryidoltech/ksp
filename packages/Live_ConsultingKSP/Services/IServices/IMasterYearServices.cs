using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IMasterYearServices
    {
        Task<IEnumerable<MasterYear>> GetAllYearsAsync(string cmpuuid, string envuuid);
        Task<MasterYear?> GetYearByUuidAsync(Guid uuid);
        Task AddYearAsync(MasterYear years);
        Task UpdateYearAsync(MasterYear model);
        void DeletedYear(string uuid);

    }
}
