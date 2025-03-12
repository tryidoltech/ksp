using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IBdResearchChannelTypeService
    {
        void AddChannelType(BdResearchChannelType channelType);
        List<BdResearchChannelType> GetAllChannelTypes(string cmpuuid, string envuuid);
        BdResearchChannelType GetChannelTypeByUUID(string uuid);
        void UpdateChannelType(BdResearchChannelType channelType);
        void DeleteChannelType(string uuid);
    }
}
