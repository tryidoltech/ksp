using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services.IServices
{
    public interface IMasterEnvironmentServices
    {
        Task<IEnumerable<MasterEnvironment>> GetAllEnvironmentsAsync();
        Task<MasterEnvironment?> GetEnvironmentByUuidAsync(Guid uuid);
        Task AddEnvironmentAsync(MasterEnvironment environment);
        Task UpdateEnvironmentAsync(MasterEnvironment model);
        void DeleteEnvironment(Guid uuid);
    }
}
