using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IResearchCommunicationModeService
    {
        void AddCommunicationMode(BdResearchCommunicationMode mode);
        List<BdResearchCommunicationMode> GetAllCommunicationModes(string cmpuuid, string envuuid);
        BdResearchCommunicationMode GetCommunicationModeByUUID(Guid uuid);
        void UpdateCommunicationMode(BdResearchCommunicationMode mode);
        void DeleteCommunicationMode(Guid uuid);
    }
}
