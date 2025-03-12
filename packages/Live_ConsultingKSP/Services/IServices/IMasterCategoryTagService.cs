using Live_ConsultingKSP.Models;

namespace Live_ConsultingKSP.Services
{
    public interface IMasterCategoryTagService
    {
        List<MasterCategorTag> GetAllCategoryTags(string cmpuuid, string envuuid);
        MasterCategorTag GetCategoryTagByUUID(Guid uuid);
        void AddCategoryTag(MasterCategorTag categoryTag);
        void UpdateCategoryTag(MasterCategorTag categoryTag);
        void DeleteCategoryTag(Guid uuid);
    }
}
