using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Service.IService
{
    public interface IMasterBanktypeServices
    {
        Task<IEnumerable<MasterBanktype>> GetAllBanktypesAsync(string cmpuuid, string envuuid);
        Task<MasterBanktype?> GetBanktypeByUuidAsync(Guid uuid);
        Task AddBankTypeAsync(MasterBanktype banktype);
        Task UpdateBankTypeAsync(MasterBanktype model);
        void Deletebanktype(Guid uuid);
    }
}
