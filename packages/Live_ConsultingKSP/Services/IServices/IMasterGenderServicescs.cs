using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Service.IService
{
    public interface IMasterGenderServicescs
    {
        Task<IEnumerable<MasterGender>> GetAllGendersAsync(string cmpuuid, string envuuid);
        Task<MasterGender?> GetGenderByUuidAsync(Guid uuid);
        Task AddGenderAsync(MasterGender gender);
        Task UpdateGenderAsync(MasterGender model);
        void Deletegender(Guid uuid);

    }
}
