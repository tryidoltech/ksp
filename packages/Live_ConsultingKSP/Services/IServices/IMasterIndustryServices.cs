using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IMasterIndustryServices
    {
        Task<List<MasterIndustry>> GetAllIndustriesAsync(string cmpuuid, string envuuid);
        Task<MasterIndustry> GetIndustryByUuidAsync(Guid uuid);
        Task AddIndustryAsync(MasterIndustry model);
        Task UpdateIndustryAsync(MasterIndustry model);
        Task DeleteIndustryAsync(string uuid);


    }
}
