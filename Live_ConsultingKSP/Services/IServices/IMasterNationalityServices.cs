using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Service.IService
{
    public interface IMasterNationalityServices
    {
        Task<IEnumerable<MasterNationality>> GetAllNationalitysAsync(string cmpuuid, string envuuid);
        Task<MasterNationality?> GetNationalityByUuidAsync(Guid uuid);
        Task AddNationalityAsync(MasterNationality nationality);
        Task UpdateNationalityAsync(MasterNationality model);
        void DeleteNationality(Guid uuid);
    }
}
