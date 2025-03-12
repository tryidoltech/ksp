using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services.IServices
{
    public interface IMasterCompanyService
    {
        Task<List<MasterCompany>> GetAllCompanyAsync();
        Task<MasterCompany?> GetCompanyByUuidAsync(Guid uuid);
        Task AddCompanyAsync(MasterCompany company, IFormFile logoFile, IFormFile stampFile, IFormFile signatureFile);
        Task UpdateCompanyAsync(MasterCompany model);
        void DeleteCompany(Guid uuid);
       

    }
}
