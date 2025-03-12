using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Service.IService
{
    public interface IMasterDocumentGroupServices
    {
        Task<IEnumerable<MasterDocumentGrop>> GetAllDocumentGropsAsync(string cmpuuid, string envuuid);
        Task<MasterDocumentGrop?> GetDocumentGropsByUuidAsync(Guid uuid);
        Task AddDocumentGropAsync(MasterDocumentGrop document);
        Task UpdateDocumentGropAsync(MasterDocumentGrop model);
        void Deletedocument(Guid uuid);
    }
}
