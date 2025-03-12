using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Service.IService
{
    public interface IMasterBloodGroupServices
    {
        Task<IEnumerable<MasterBloodGroup>> GetAllBloodGroupsAsync(string cmpuuid, string envuuid);
        Task<MasterBloodGroup?> GetBloodGroupByUuidAsync(Guid uuid);
        Task AddBloodGroupAsync(MasterBloodGroup bloodGroup);
        Task UpdateBloodGroupAsync(MasterBloodGroup model);
        void DeleteBloodGroup(Guid uuid);
    }
}
