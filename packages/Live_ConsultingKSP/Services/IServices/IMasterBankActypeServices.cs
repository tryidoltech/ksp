using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Service.IService
{
    public interface IMasterBankActypeServices
    {
        Task<IEnumerable<MasterBankActype>> GetAllBankActypesAsync(string cmpuuid, string envuuid);
        Task<MasterBankActype?> GetBankActypeByUuidAsync(Guid uuid);
        Task AddBankAcTypeAsync(MasterBankActype bankActype);
        Task UpdateBankAcTypeAsync(MasterBankActype model);
        void Deletebankactype(Guid uuid);

    }
}
