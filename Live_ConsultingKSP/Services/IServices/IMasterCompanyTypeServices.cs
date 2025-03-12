using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services.IServices
{
    public interface IMasterCompanyTypeServices
    {
        Task<IEnumerable<MasterCompanyType>> GetAllCompanyTypesAsync(string cmpuuid, string envuuid);
        Task<MasterCompanyType?> GetCompanyTypeByUuidAsync(Guid uuid);
        Task AddCompanyTypeAsync(MasterCompanyType companyType);
        Task UpdateCompanyTypeAsync(MasterCompanyType model);
        void DeleteCompanyType(Guid uuid);
    }
}
