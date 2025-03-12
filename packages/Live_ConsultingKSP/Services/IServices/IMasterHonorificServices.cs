using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Service.IService
{
    public interface IMasterHonorificServices
    {
        Task<IEnumerable<MasterHonorific>> GetAllHonorificsAsync(string cmpuuid, string envuuid);
        Task<MasterHonorific?> GetHonorificByUuidAsync(Guid uuid);
        Task AddNewHonorificAsync(MasterHonorific honorific);
        Task UpdateHonorificAsync(MasterHonorific model);
        void Deletehonorific(Guid uuid);
    }
}
