using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IHeadDesignationService
    {
        void AddHeadDesignation(ErHeadDesignation designation);
        Task<IEnumerable<ErHeadDesignation>> GetAllHeadDesignations(string cmpuuid, string envuuid);
            ErHeadDesignation GetHeadDesignationByUUID(string uuid);
        void UpdateHeadDesignation(ErHeadDesignation designation);
        void DeleteHeadDesignation(string uuid);
    }
}
