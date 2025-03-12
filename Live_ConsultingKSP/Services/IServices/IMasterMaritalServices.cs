using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Service.IService
{
    public interface IMasterMaritalServices
    {
        Task<IEnumerable<MasterMarital>> GetAllMaritalAsync(string cmpuuid, string envuuid);
        Task<MasterMarital?> GetMaritalByUuidAsync(Guid uuid);
        Task AddMaritalAsync(MasterMarital marital);
        Task UpdateMaritalAsync(MasterMarital model);
        void DeleteMarital(Guid uuid);

    }
}
